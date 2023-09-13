// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JEMeshRenderer : JEComponent
{
    public JEMesh mesh;
    public List<JEMaterial> Materials = new List<JEMaterial>();

    override public void Preprocess()
    {
        unityMeshRenderer = unityComponent as MeshRenderer;
        unityMeshFilter = jeGameObject.unityGameObject.GetComponent<MeshFilter>();

        if (unityMeshFilter == null)
        {
            ExportError.FatalError("MeshRenderer with no MeshFilter");
        }

    }

    override public void QueryResources()
    {
        mesh = JEMesh.RegisterMesh(unityMeshFilter.sharedMesh);

        for (int i = 0; i < unityMeshRenderer.sharedMaterials.Length; i++)
        {
            Materials.Add (JEMaterial.RegisterMaterial(unityMeshRenderer.sharedMaterials[i]));
        }
    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONMeshRenderer();
        json.type = "MeshRenderer";

        json.mesh = mesh.name;
        json.enabled = unityMeshRenderer.enabled;
        json.castShadows = unityMeshRenderer.castShadows;
        json.receiveShadows = unityMeshRenderer.receiveShadows;
        json.lightmapIndex = unityMeshRenderer.lightmapIndex;
        json.lightmapTilingOffset = unityMeshRenderer.lightmapScaleOffset;
        json.materials = new string[Materials.Count];
        for (int i = 0; i < Materials.Count; i++)
        {
            json.materials[i] = Materials[i].name;
        }

        return json;
    }

    MeshRenderer unityMeshRenderer;
    MeshFilter unityMeshFilter;
}

}
