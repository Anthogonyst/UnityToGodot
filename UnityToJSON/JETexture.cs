// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JETexture : JEResource
{

    private JETexture(Texture texture)
    {
        this.unityTexture = texture as Texture2D;
        allTextures[texture] = this;
        name = texture.name;
    }

    void preprocess()
    {
        //Debug.Log("preprocess - " + unityTexture);
    }

    void process()
    {
        //Debug.Log("process - " + unityTexture);

        name = unityTexture.name;

        string path = AssetDatabase.GetAssetPath(unityTexture);

        TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

        if (textureImporter.isReadable == false)
        {
            textureImporter.isReadable = true;
            AssetDatabase.ImportAsset(path);
        }

        Texture2D ntexture = new Texture2D(unityTexture.width, unityTexture.height, TextureFormat.ARGB32, false);
        ntexture.SetPixels32(unityTexture.GetPixels32());
        ntexture.Apply();


        var bytes = ntexture.EncodeToPNG();

        base64PNGLength = bytes.Length;
        base64PNG =  System.Convert.ToBase64String(bytes, 0, bytes.Length);


        UnityEngine.Object.DestroyImmediate(ntexture);

    }

    void postprocess()
    {
        //Debug.Log("postprocess - " + unityTexture);
    }


    public static JETexture RegisterTexture(Texture texture)
    {
        if (allTextures.ContainsKey(texture))
            return allTextures[texture];

        return new JETexture(texture);
    }

    new public static void Preprocess()
    {
        foreach (var texture in allTextures.Values)
        {
            texture.preprocess();
        }

    }

    new public static void Process()
    {
        foreach (var texture in allTextures.Values)
        {
            texture.process();
        }

    }

    new public static void PostProcess()
    {
        foreach (var texture in allTextures.Values)
        {
            texture.postprocess();
        }
    }

    new public static void Reset()
    {
        allTextures = new Dictionary<Texture, JETexture >();
    }

    public new JSONTexture ToJSON()
    {
        var json = new JSONTexture();

        json.name = name;
        json.base64PNG = base64PNG;
        json.base64PNGLength = base64PNGLength;

        return json;
    }

    public static List<JSONTexture> GenerateJSONTextureList()
    {
        List<JSONTexture> textures = new List<JSONTexture>();

        foreach (var texture in allTextures.Values)
            textures.Add(texture.ToJSON());

        return textures;
    }

    Texture2D unityTexture;

    public string name;
    public string base64PNG;
    public int base64PNGLength;

    public static Dictionary<Texture, JETexture> allTextures;

}


}
