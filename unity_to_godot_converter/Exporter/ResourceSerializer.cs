using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Godot
{
    interface IConvertedResourceProvider
    {
        Resource GetConvertedResource(string path);
    }

    class ResourceSerializer
    {
        IConvertedResourceProvider _resourceProvider;
        StringBuilder _sb = new StringBuilder();

        public ResourceSerializer(IConvertedResourceProvider provider)
        {
            _resourceProvider = provider;
        }

        public void SaveGodotResource(Resource res, string path)
        {
            var item = new Item();
            item.type = "resource";
            item.properties = res.GetData();
            SaveGodotResource(path, res.GetType().Name, new List<Item> { item });
        }

        public void SaveGodotScene(Node node, string path)
        {
            var items = new List<Item>();
            var nodePath = new List<string>();
            GatherNodeItems(node, items, nodePath);
            SaveGodotResource(path, null, items);
        }

        void GatherNodeItems(Node node, List<Item> items, List<string> nodePath)
        {
            var item = new Item();
            item.type = "node";
            item.headerProperties = new Dictionary<string, object>
            {
                { "name", node.name },
                { "type", node.GetType().Name }
            };
            if(nodePath.Count == 1)
            {
                item.headerProperties["parent"] = ".";
            }
            else if (nodePath.Count > 1)
            {
                item.headerProperties["parent"] = ConcatNodePath(nodePath, 1);
            }
            item.properties = node.GetData();
            items.Add(item);

            nodePath.Add(node.name);
            int last = nodePath.Count - 1;

            foreach (var child in node.children)
            {
                GatherNodeItems(child, items, nodePath);
            }

            nodePath.RemoveAt(last);
        }

        string ConcatNodePath(List<string> nodePath, int from)
        {
            _sb.Length = 0;
            for(int i = from; i < nodePath.Count; ++i)
            {
                if(i != from)
                {
                    _sb.Append("/");
                }
                _sb.Append(nodePath[i]);
            }
            return _sb.ToString();
        }

        class Item
        {
            public string type;
            public Dictionary<string, object> headerProperties;
            public Dictionary<string, object> properties;
        }

        void SaveGodotResource(string path, string mainResourceType, List<Item> items)
        {
            //Debug.Log("Saving " + path);

            int nextId = 1;
            var extResourceIds = new Dictionary<string, int>();

            foreach(var item in items)
            {
                if(item.properties == null)
                {
                    continue;
                }

                foreach (var kv in item.properties)
                {
                    if (kv.Value is Godot.Resource)
                    {
                        var res = (Godot.Resource)kv.Value;

                        if(!extResourceIds.ContainsKey(res.resourcePath))
                        {
                            extResourceIds[res.resourcePath] = nextId;
                            ++nextId;
                        }
                    }
                }
            }

            int loadSteps = extResourceIds.Count;

            using (StreamWriter sw = Util.CreateStreamWriter(path))
            {
                // TODO Meh
                if(mainResourceType != null)
                {
                    sw.WriteLine(string.Format("[gd_resource type=\"{0}\" load_steps={1} format=2]", mainResourceType, loadSteps));
                }
                else
                {
                    sw.WriteLine(string.Format("[gd_scene load_steps={1} format=2]", mainResourceType, loadSteps));
                }

                sw.WriteLine("");

                foreach (var kv in extResourceIds)
                {
                    string extResourcePath = kv.Key;
                    int id = kv.Value;

                    Resource res = _resourceProvider.GetConvertedResource(extResourcePath);

                    sw.WriteLine(string.Format("[ext_resource path=\"res://{0}\" type=\"{1}\" id={2}]", res.resourcePath, res.GetType().Name, id));
                }

                sw.WriteLine("");

                foreach (var item in items)
                {
                    sw.Write("[");
                    sw.Write(item.type);

                    if(item.headerProperties != null)
                    {
                        foreach(var kv in item.headerProperties)
                        {
                            sw.Write(string.Format(" {0}=", kv.Key));
                            SerializeGodotVariant(kv.Value, sw, extResourceIds);
                        }
                    }

                    sw.WriteLine("]");
                    sw.WriteLine("");

                    if (item.properties != null)
                    {
                        foreach (var kv in item.properties)
                        {
                            var value = kv.Value;

                            sw.Write(kv.Key);
                            sw.Write(" = ");

                            SerializeGodotVariant(value, sw, extResourceIds);

                            sw.WriteLine();
                        }
                    }

                    sw.WriteLine();
                }
            }
        }

        void SerializeGodotVariant(object value, StreamWriter sw, Dictionary<string, int> extResourceIds)
        {
            if (value is int || value is float || value is double)
            {
                sw.Write(value);
            }
            else if (value is Vector2)
            {
                var v = (Vector2)value;
                sw.Write(string.Format("Vector2( {0}, {1} )", v.x, v.y));
            }
            else if (value is Vector3)
            {
                var v = (Vector3)value;
                sw.Write(string.Format("Vector3( {0}, {1}, {2} )", v.x, v.y, v.z));
            }
            else if (value is Color)
            {
                var v = (Color)value;
                sw.Write(string.Format("Color( {0}, {1}, {2}, {3} )", v.r, v.g, v.b, v.a));
            }
            else if (value is Rect)
            {
                var v = (Rect)value;
                sw.Write(string.Format("Rect2( {0}, {1}, {2}, {3} )", v.x, v.y, v.width, v.height));
            }
            else if (value is string)
            {
                var v = (string)value;
                v = v.Replace("\"", "\\\"");
                sw.Write(string.Format("\"{0}\"", v));
            }
            else if (value is bool)
            {
                var v = (bool)value;
                if (v)
                    sw.Write("true");
                else
                    sw.Write("false");
            }
            else if (value is Resource)
            {
                var v = (Resource)value;
                int id = extResourceIds[v.resourcePath];
                sw.Write(string.Format("ExtResource( {0} )", id));
            }
            else if (value == null)
            {
                sw.Write("null");
            }
            else if (value is List<object>)
            {
                var v = (List<object>)value;
                sw.Write("[ ");
                for (int i = 0; i < v.Count; ++i)
                {
                    if (i != 0)
                    {
                        sw.Write(", ");
                    }

                    SerializeGodotVariant(v[i], sw, extResourceIds);
                }
                sw.Write("]");
            }
            else if (value is Dictionary<string, object>)
            {
                var v = (Dictionary<string, object>)value;
                sw.WriteLine("{");
                int i = 0;
                foreach (var kv in v)
                {
                    if (i != 0)
                    {
                        sw.WriteLine(",");
                    }

                    SerializeGodotVariant(kv.Value, sw, extResourceIds);

                    ++i;
                }
                sw.WriteLine();
                sw.WriteLine("}");
            }
            else
            {
                Debug.LogError("Unsupported type " + value);
            }
        }
    }
}
