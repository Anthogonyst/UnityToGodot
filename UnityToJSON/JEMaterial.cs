// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JEMaterial : JEResource
{
    public Material unityMaterial;
    public int index;

    public JETexture texture;
    public JEShader shader;

    private JEMaterial(Material material)
    {
        this.unityMaterial = material;
        this.index = allMaterials.Count;
        this.name = material.name;
        allMaterials[material] = this;

        Texture2D _texture = material.mainTexture as Texture2D;

        if (_texture != null)
            texture = JETexture.RegisterTexture(_texture);

        shader = JEShader.RegisterShader(material.shader);

    }

    void preprocess()
    {
        //Debug.Log("preprocess - " + unityMaterial);
    }

    void process()
    {
        //Debug.Log("process - " + unityMaterial);
    }

    void postprocess()
    {
        //Debug.Log("postprocess - " + unityMaterial);
    }

    new public static void Preprocess()
    {
        foreach (var material in allMaterials.Values)
        {
            material.preprocess();
        }
    }

    new public static void Process()
    {
        foreach (var material in allMaterials.Values)
        {
            material.process();
        }

    }

    new public static void PostProcess()
    {
        foreach (var material in allMaterials.Values)
        {
            material.postprocess();
        }
    }

    new public static void Reset()
    {
        allMaterials = new Dictionary<Material, JEMaterial >();
    }

    public static JEMaterial RegisterMaterial(Material material)
    {
        // we're trying to go off sharedMaterials, but we're getting
        // "instance" materials or something between meshes that share a material
        // so go off name for now
        foreach (Material m in allMaterials.Keys)
        {
            if (m.name == material.name)
                return allMaterials[m];
        }

        return new JEMaterial(material);
    }

    public new JSONMaterial ToJSON()
    {
        var json = new JSONMaterial();

        json.name = name;

        json.shader = shader.name;
        json.mainTexture = texture == null ? "" : texture.name;
        json.mainTextureOffset = unityMaterial.mainTextureOffset;
        json.mainTextureScale = unityMaterial.mainTextureScale;
        json.passCount = unityMaterial.passCount;
        json.renderQueue = unityMaterial.renderQueue;

        if (unityMaterial.shaderKeywords.Length > 0)
        {
            json.shaderKeywords = new string[unityMaterial.shaderKeywords.Length];
            for (int i = 0; i < unityMaterial.shaderKeywords.Length;i++)
                json.shaderKeywords[i] = unityMaterial.shaderKeywords[i];
        }

        //json.color = unityMaterial.color;

        return json;
    }

    public static List<JSONMaterial> GenerateJSONMaterialList()
    {
        List<JSONMaterial> materials = new List<JSONMaterial>();

        foreach (var material in allMaterials.Values)
            materials.Add(material.ToJSON());

        return materials;
    }

    public static Dictionary<Material, JEMaterial> allMaterials;


}

}
