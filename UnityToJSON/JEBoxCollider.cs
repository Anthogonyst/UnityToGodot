// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JEBoxCollider : JEComponent
{

    override public void Preprocess()
    {
        unityBoxCollider = unityComponent as BoxCollider;
    }

    override public void QueryResources()
    {
    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONBoxCollider();
        json.type = "BoxCollider";

        json.size = unityBoxCollider.size;
        json.center = unityBoxCollider.center;


        return json;
    }

    BoxCollider unityBoxCollider;
}

}
