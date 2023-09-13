using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Godot
{
    class Exporter : IConvertedResourceProvider
    {
        [MenuItem("Godot/Export to Godot 3.1")]
        static void ExportToGodot31()
        {
            var e = new Exporter("D:/PROJETS/INFO/UNITY/LD32_EatAndCopulate/GodotProject");
            e.Export();
        }

        enum ScriptLanguage
        {
            GDScript,
            CSharp
        }

        /// <summary>
        /// Some components are ambiguous between Godot and Unity, such as Camera, which could either be 2D or 3D.
        /// If they can't be determined, this decides which type is preferred.
        /// </summary>
        bool _hint2D = true;
        ScriptLanguage _scriptLanguage = ScriptLanguage.GDScript;
        string _projectDir;
        Dictionary<string, Resource> _convertedAssets = new Dictionary<string, Resource>();
        // Had to make a separate collection for sprites because they don't have a unique path
        Dictionary<UnityEngine.Sprite, Resource> _convertedSprites = new Dictionary<UnityEngine.Sprite, Resource>();

        Exporter(string targetProjectDir)
        {
            _projectDir = targetProjectDir;
        }

        public void Export()
        {
            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                ConvertScene(scene);
            }

            Debug.Log("Creating project file...");
            CreateProjectFile();

            Debug.Log("Done");
        }

        public Resource GetConvertedResource(string path)
        {
            return _convertedAssets[path];
        }
        
        void CreateProjectFile()
        {
            string mainScenePath = "";
            if(SceneManager.sceneCountInBuildSettings > 0)
            {
                Scene mainScene = SceneManager.GetSceneByBuildIndex(0);
                mainScenePath = Path.Combine(_projectDir, mainScene.path);
            }

            string path = Path.Combine(_projectDir, "project.godot");
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.NewLine = "\n";

                sw.WriteLine("config_version=4");

                sw.WriteLine("");

                sw.WriteLine("[application]");

                sw.WriteLine();

                sw.WriteLine(string.Format("config/name = \"{0}\"", Application.productName));

                if(mainScenePath != "")
                {
                    sw.WriteLine(string.Format("run/main_scene = \"{0}\"", mainScenePath));
                }
            }
        }

        void ConvertScene(Scene scene)
        {
            Debug.Log("Loading scene " + scene.path);
            //EditorSceneManager.OpenScene(scene.path, OpenSceneMode.Single);

            string targetPath = Path.ChangeExtension(Path.Combine(_projectDir, scene.path), ".tscn");

            var tree = new Node();
            tree.name = scene.name;

            GameObject[] rootObjects = scene.GetRootGameObjects();

            foreach(GameObject go in rootObjects)
            {
                var node = ExtractNodeBranch(go);
                if(node != null)
                {
                    node.name = go.name;
                    tree.AddChild(node);
                }
            }

            // TODO If the scene contains one camera, make it current

            new ResourceSerializer(this).SaveGodotScene(tree, targetPath);
        }

        enum NodeCategory
        {
            None,
            Node2D,
            Spatial,
            Control
        }

        Node ExtractNodeBranch(GameObject go)
        {
            // TODO Detect if go is a prefab with PrefabUtility

            var componentNodes = new List<Node>();
            Component[] components = go.GetComponents<Component>();

            NodeCategory rootCategory = DetectCategory(go);

            Node rootNode = null;
            Rigidbody2D promoteRigidBody2D = null;
            float rescale2d = 1f;
            int scriptCount = 0;
            Script script = null;
            Dictionary<string, object> scriptVariables = null;

            foreach(var cmp in components)
            {
                Node node = null;

                if(cmp is SpriteRenderer)
                {
                    var src = (SpriteRenderer)cmp;
                    var dst = new Sprite();
                    node = dst;

                    dst.selfModulate = src.color;

                    if (src.sprite != null)
                    {
                        dst.texture = Util.Cast<Texture>(ConvertUnityAsset(src.sprite));
                        rescale2d = src.sprite.pixelsPerUnit;
                    }
                }
                else if(cmp is Rigidbody2D)
                {
                    // In Godot, bodies work best as root
                    promoteRigidBody2D = (Rigidbody2D)cmp;
                }
                else if(cmp is Collider2D)
                {
                    // TODO Proper CollisionShape2D
                    node = new CollisionShape2D();
                }
                else if(cmp is Camera)
                {
                    var cam = (Camera)cmp;
                    if((rootCategory == NodeCategory.Node2D || _hint2D) && cam.orthographic)
                    {
                        node = new Camera2D();
                    }
                    else
                    {
                        // TODO 3D Camera
                    }
                }
                else if(cmp is MonoBehaviour)
                {
                    var mb = (MonoBehaviour)cmp;
                    var s = ConvertScript(mb);

                    if (s != null)
                    {
                        if (scriptCount == 0)
                        {
                            script = s;
                            scriptVariables = GetScriptVariables(mb);
                        }
                        else
                        {
                            node = new Node();
                            node.name = cmp.GetType().Name;
                            node.script = s;
                            node.scriptVariables = GetScriptVariables(mb);
                        }

                        ++scriptCount;
                    }
                }
                else
                {
                    // TODO more
                }

                if (node != null)
                {
                    componentNodes.Add(node);
                }
            }

            if (rootCategory == NodeCategory.Node2D)
            {
                Node2D node2d = null;

                if (promoteRigidBody2D != null)
                {
                    if(promoteRigidBody2D.isKinematic)
                    {
                        var n = new KinematicBody2D();
                        node2d = n;
                    }
                    else
                    {
                        var n = new RigidBody2D();
                        node2d = n;
                    }
                }
                else
                {
                    if(componentNodes.Count == 1 && componentNodes[0] is Node2D && !(script != null && componentNodes[0].script != null))
                    {
                        // If there is only one component node, don't create a root node with a single child

                        // TODO Merge transform? Not sure if relevant because this is components
                        node2d = (Node2D)componentNodes[0];
                        componentNodes.Clear();
                    }
                    else
                    {
                        node2d = new Node2D();
                    }
                }

                node2d.position = go.transform.position * rescale2d;
                node2d.position.y *= -1f;
                // TODO Invert Y properly
                // TODO Select pivot
                //node2d.position.y = Screen.height - node2d.position.y;

                node2d.scale = go.transform.localScale;

                // TODO Is eulerAngles in degrees or radians??
                node2d.rotation = go.transform.rotation.eulerAngles.z;

                node2d.visible = go.activeSelf;

                rootNode = node2d;
            }
            else if(rootCategory == NodeCategory.Control)
            {
                var control = new Control();
                control.visible = go.activeSelf;
                rootNode = control;
            }
            else if(rootCategory == NodeCategory.Spatial)
            {
                var spatial = new Spatial();
                rootNode = spatial;
            }
            else
            {
                rootNode = new Node();
            }

            if(rootNode != null)
            {
                rootNode.name = go.name;

                if(script != null)
                {
                    rootNode.script = script;
                }
                if(scriptVariables != null)
                {
                    rootNode.scriptVariables = scriptVariables;
                }

                foreach(var child in componentNodes)
                {
                    if(child.name == null)
                    {
                        child.name = child.GetType().Name;
                    }
                    rootNode.AddChild(child);
                }
            }

            // Children
            Transform transform = go.transform;
            for(int i = 0; i < transform.childCount; ++i)
            {
                GameObject childGo = transform.GetChild(i).gameObject;
                Node childNode = ExtractNodeBranch(childGo);
                rootNode.AddChild(childNode);
            }

            return rootNode;
        }

        NodeCategory DetectCategory(Component[] components)
        {
            int filteredCount = 0;
            foreach(var cmp in components)
            {
                if(cmp is RectTransform)
                {
                    return NodeCategory.Control;
                }
                // Doing the assumption that if you have any of these components on a game object,
                // you are probably not using it for 3D, or that would be really confusing
                if(cmp is Collider2D || cmp is SpriteRenderer || cmp is Rigidbody2D)
                {
                    return NodeCategory.Node2D;
                }

                if(cmp is MeshFilter || cmp is MeshRenderer || cmp is Rigidbody || cmp is Collider)
                {
                    return NodeCategory.Spatial;
                }

                // The following components are ambiguous to wether they should be translated as 2D or 3D nodes
                if(_hint2D && ((cmp is Camera && ((Camera)cmp).orthographic) || cmp is AudioSource))
                {
                    return NodeCategory.Node2D;
                }

                if(!(cmp is MonoBehaviour || cmp is Transform))
                {
                    ++filteredCount;
                }
            }

            if(filteredCount == 0)
            {
                return NodeCategory.None;
            }
            else
            {
                return NodeCategory.Spatial;
            }
        }

        NodeCategory DetectCategory(GameObject go)
        {
            NodeCategory cat = DetectCategory(go.GetComponents<Component>());

            if(cat != NodeCategory.None)
            {
                return cat;
            }

            var t = go.transform;
            for(int i = 0; i < t.childCount; ++i)
            {
                cat = DetectCategory(t.GetChild(i).gameObject);
                if(cat != NodeCategory.None)
                {
                    return cat;
                }
            }

            return cat;
        }

        //static bool Is2D(NodeCategory cat)
        //{
        //    return cat == NodeCategory.Control || cat == NodeCategory.Node2D;
        //}

        System.Type ConvertUnityType(System.Type t)
        {
            if(t == typeof(UnityEngine.Sprite))
            {
                return typeof(AtlasTexture);
            }
            else if(t == typeof(UnityEngine.Texture))
            {
                return typeof(Texture);
            }
            else
            {
                return typeof(Resource);
            }
        }

        Resource ConvertUnityAsset(UnityEngine.Object obj)
        {
            if(obj is UnityEngine.Sprite)
            {
                return ConvertSpriteAsset((UnityEngine.Sprite)obj);
            }
            else if(obj is UnityEngine.Texture)
            {
                return ConvertTexture((UnityEngine.Texture)obj);
            }
            else if(obj is MonoBehaviour)
            {
                return ConvertScript((MonoBehaviour)obj);
            }
            else
            {
                // TODO
                return null;
            }
        }

        Resource ConvertSpriteAsset(UnityEngine.Sprite sprite)
        {
            if (_convertedSprites.ContainsKey(sprite))
            {
                return _convertedSprites[sprite];
            }

            Texture2D texture = sprite.texture;
            Texture atlas = (Godot.Texture)ConvertTexture(texture);

            string relPath = AssetDatabase.GetAssetPath(texture);
            relPath = string.Format("{0}__{1}.tres", relPath.Substring(0, relPath.Length - Path.GetExtension(relPath).Length), sprite.name);

            var at = new AtlasTexture();
            at.resourcePath = relPath;
            at.atlas = atlas;
            at.region = sprite.textureRect;

            // Invert Y
            at.region.y = sprite.texture.height - at.region.y - at.region.height;

            new ResourceSerializer(this).SaveGodotResource(at, Path.Combine(_projectDir, relPath));

            _convertedSprites.Add(sprite, at);
            _convertedAssets.Add(at.resourcePath, at);

            return at;
        }

        Resource ConvertTexture(UnityEngine.Texture texture)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            if (texture is Texture2D)
            {
                var tex = (Texture2D)texture;

                Resource res = null;
                if (!_convertedAssets.TryGetValue(path, out res))
                {
                    CopyAsset(tex);

                    res = new Texture();
                    res.resourcePath = path;

                    _convertedAssets.Add(path, res);
                    // TODO Import settings
                }

                return res;
            }
            else
            {
                // TODO
            }

            return null;
        }

        List<FieldInfo> GetScriptFields(MonoBehaviour mb)
        {
            var filteredFields = new List<FieldInfo>();

            FieldInfo[] fields = mb.GetType().GetFields(
                BindingFlags.Instance 
                | BindingFlags.FlattenHierarchy 
                | BindingFlags.Public 
                | BindingFlags.NonPublic);

            //Debug.Log("Fields of " + mb.GetType() + ": " + fields.Length);

            foreach (var field in fields)
            {
                if (field.GetCustomAttributes(typeof(SerializeField), false).Length != 0 || field.IsPublic)
                {
                    System.Type t = field.FieldType;

                    if (t == typeof(int)
                        || t == typeof(bool)
                        || t == typeof(float)
                        || t == typeof(double)
                        || t == typeof(string)
                        || t == typeof(Vector2)
                        || t == typeof(Vector3)
                        || t == typeof(Color)
                        || t == typeof(Rect)
                        )
                    {
                        filteredFields.Add(field);
                    }
                    else if (IsUnityObjectType(t))
                    {
                        filteredFields.Add(field);
                    }
                }
            }
            return filteredFields;
        }

        static bool IsUnityObjectType(System.Type t)
        {
            return typeof(UnityEngine.Object).IsAssignableFrom(t);
        }

        Dictionary<string, object> GetScriptVariables(MonoBehaviour mb)
        {
            var d = new Dictionary<string, object>();
            List<FieldInfo> fields = GetScriptFields(mb);
            foreach(var field in fields)
            {
                var v = field.GetValue(mb);
                if(v is UnityEngine.Object)
                {
                    var res = ConvertUnityAsset((UnityEngine.Object)v);
                    d[field.Name] = res;
                }
                else
                {
                    d[field.Name] = v;
                }
            }
            return d;
        }

        Script ConvertScript(MonoBehaviour mb)
        {
            MonoScript mbScript = MonoScript.FromMonoBehaviour(mb);
            string originalPath = AssetDatabase.GetAssetPath(mbScript);

            if(originalPath.EndsWith(".dll"))
            {
                // TODO This may happen on UI components, these should be handled explicitely in the exporter
                Debug.LogWarning("Can't convert " + mb.GetType() + " as a regular script");
                return null;
            }

            Debug.Log("Path to MB " + originalPath);

            string targetPath = Path.Combine(_projectDir, originalPath);
            string resPath;

            if(_scriptLanguage == ScriptLanguage.GDScript)
            {
                targetPath = Path.ChangeExtension(targetPath, ".gd");
                resPath = Path.ChangeExtension(originalPath, ".gd");

                if (_convertedAssets.ContainsKey(resPath))
                {
                    return (Script)_convertedAssets[resPath];
                }

                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));

                using (StreamWriter sw = Util.CreateStreamWriter(targetPath))
                {
                    sw.WriteLine("extends Node");
                    sw.WriteLine("");

                    List<FieldInfo> fields = GetScriptFields(mb);
                    foreach(var field in fields)
                    {
                        sw.Write(string.Format("export var {0}: ", field.Name));

                        var t = field.FieldType;
                        
                        if(t == typeof(GameObject))
                        {
                            sw.Write("NodePath");
                        }
                        else if(IsUnityObjectType(t))
                        {
                            sw.Write(ConvertUnityType(t).Name);
                        }
                        else if (t == typeof(int))
                        {
                            sw.Write("int");
                        }
                        else if (t == typeof(float) || t == typeof(double))
                        {
                            sw.Write("float");
                        }
                        else if (t == typeof(bool))
                        {
                            sw.Write("bool");
                        }
                        else if (t == typeof(string))
                        {
                            sw.Write("String");
                        }
                        else
                        {
                            sw.Write(t.Name);
                        }

                        sw.WriteLine();
                    }

                    sw.WriteLine();

                    string[] originalCode = File.ReadAllLines(originalPath);
                    foreach(string line in originalCode)
                    {
                        sw.Write("# ");
                        sw.WriteLine(line);
                    }
                }
            }
            else
            {
                throw new System.Exception("C# conversion not supported yet");
            }

            var script = new Script();
            script.resourcePath = resPath;
            _convertedAssets.Add(resPath, script);

            return script;
        }

        string CopyAsset(UnityEngine.Object obj)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            string targetPath = Path.Combine(_projectDir, path);
            string targetDir = Path.GetDirectoryName(targetPath);
            Directory.CreateDirectory(targetDir);
            File.Copy(path, targetPath);
            return path;
        }
    }
}
