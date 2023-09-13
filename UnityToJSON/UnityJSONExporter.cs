// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class UnityJSONExporter : ScriptableObject
{
    static void reset()
    {
        JEResource.Reset();
        JEComponent.Reset();
        JEScene.Reset();
        JEGameObject.Reset();

        JEComponent.RegisterStandardComponents();
    }

    public static JSONScene GenerateJSONScene()
    {
        // reset the exporter in case there was an error, Unity doesn't cleanly load/unload editor assemblies
        reset();

        JEScene.sceneName = Path.GetFileNameWithoutExtension(EditorApplication.currentScene);

        JEScene scene = JEScene.TraverseScene();

        scene.Preprocess();
        scene.Process();
        scene.PostProcess();

        JSONScene jsonScene = scene.ToJSON() as JSONScene;

        reset();

        return jsonScene;

    }

    [MenuItem ("UnityToJSON/Export to JSON")]
    public static void DoExport()
    {

       var defaultFileName = Path.GetFileNameWithoutExtension(EditorApplication.currentScene) + ".json";

        var path = EditorUtility.SaveFilePanel(
      					"Export Scene to JSON",
      					"",
                defaultFileName,
      					"json");

  			if (path.Length != 0)
        {

          var jsonScene = GenerateJSONScene();
          JsonConverter[] converters = new JsonConverter[]{new BasicTypeConverter()};
          string json = JsonConvert.SerializeObject(jsonScene, Formatting.Indented, converters);
          System.IO.File.WriteAllText(path, json);

          EditorUtility.DisplayDialog("UnityToJSON", "Export Successful", "OK");

  			}

    }

}

}
