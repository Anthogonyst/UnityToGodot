// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JELight : JEComponent
{

    override public void Preprocess()
    {
        unityLight = unityComponent as Light;
    }

    override public void QueryResources()
    {

    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONLight();
        json.type = "Light";

        json.lightType = "Point";
        json.color = unityLight.color;
        json.range = unityLight.range;
        json.castsShadows = unityLight.shadows != LightShadows.None;
        json.realtime = true;

        SerializedObject serial = new SerializedObject(unityLight);
        SerializedProperty lightmapProp = serial.FindProperty("m_Lightmapping");
        if (lightmapProp.intValue != 0)
        {
            // not a realtime light
            json.realtime = false;
        }


        return json;
    }

    Light unityLight;
}

}
