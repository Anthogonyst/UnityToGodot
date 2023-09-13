// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JESkinnedMeshRenderer : JEComponent
{
    public JEMesh mesh;
    public List<JEMaterial> Materials = new List<JEMaterial>();

    override public void Preprocess()
    {
        unityMeshRenderer = unityComponent as SkinnedMeshRenderer;

    }

    override public void QueryResources()
    {
        mesh = JEMesh.RegisterMesh(unityMeshRenderer.sharedMesh);
        mesh.bones = unityMeshRenderer.bones;

        mesh.rootBone = unityMeshRenderer.rootBone == null ? "" : unityMeshRenderer.rootBone.gameObject.name;

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
        var json = new JSONSkinnedMeshRenderer();
        json.type = "SkinnedMeshRenderer";

        json.mesh = mesh.name;
        json.enabled = unityMeshRenderer.enabled;
        json.castShadows = unityMeshRenderer.castShadows;
        json.receiveShadows = unityMeshRenderer.receiveShadows;
        json.materials = new string[Materials.Count];
        for (int i = 0; i < Materials.Count; i++)
        {
            json.materials[i] = Materials[i].name;
        }

        return json;
    }

    SkinnedMeshRenderer unityMeshRenderer;
}

}
