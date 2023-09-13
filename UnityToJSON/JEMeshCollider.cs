// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JEMeshCollider : JEComponent
{

    override public void Preprocess()
    {
        unityMeshCollider = unityComponent as MeshCollider;
    }

    override public void QueryResources()
    {
    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONMeshCollider();
        json.type = "MeshCollider";

        return json;
    }

    MeshCollider unityMeshCollider;
}

}
