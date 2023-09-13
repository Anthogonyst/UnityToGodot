// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JELightmap : JEResource
{
    void preprocess()
    {

    }

    void process()
    {

    }

    void postprocess()
    {

    }


    // TODO: we also want to copy the exr data in case user wants it
    new public static void Preprocess()
    {
        LightmapData[] lightmaps = LightmapSettings.lightmaps;

        for (int i = 0; i < lightmaps.Length; i++)
        {
            var lightmap = lightmaps[i].lightmapNear;

            if (lightmap == null)
                lightmap = lightmaps[i].lightmapFar;

            string path = AssetDatabase.GetAssetPath(lightmap);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            TextureImporterSettings settings = new TextureImporterSettings();
            textureImporter.ReadTextureSettings(settings);

            bool setReadable = false;
            if (!settings.readable)
                setReadable = true;

            settings.readable = true;
            settings.lightmap = true;
            textureImporter.SetTextureSettings(settings);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            var pixels = lightmap.GetPixels32();


            Texture2D ntexture = new Texture2D(lightmap.width, lightmap.height, TextureFormat.ARGB32, false);
            ntexture.SetPixels32(pixels);
            ntexture.Apply();

            var bytes = ntexture.EncodeToPNG();

            UnityEngine.Object.DestroyImmediate(ntexture);

            if (setReadable)
                settings.readable = false;

            settings.lightmap = true;
            textureImporter.SetTextureSettings(settings);
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            JELightmap lm = new JELightmap();

            lm.filename = JEScene.sceneName + "_Lightmap_" + i;
            lm.base64PNGLength = bytes.Length;
            lm.base64PNG =  System.Convert.ToBase64String(bytes, 0, bytes.Length);

            allLightmaps.Add(lm);



        }

    }

    new public static void Process()
    {
    }

    new public static void PostProcess()
    {
    }

    new public static void Reset()
    {
        allLightmaps = new List<JELightmap>();
    }

    public new JSONLightmap ToJSON()
    {
        return null;
    }

    public static List<JSONLightmap> GenerateJSONLightmapList()
    {
        List<JSONLightmap> lightmaps = new List<JSONLightmap>();

        for (int i = 0; i < allLightmaps.Count; i++)
        {
            JELightmap lightmap = allLightmaps[i];
            JSONLightmap jlightmap = new JSONLightmap();

            jlightmap.filename = lightmap.filename;
            jlightmap.base64PNG = lightmap.base64PNG;
            jlightmap.base64PNGLength = lightmap.base64PNGLength;

            lightmaps.Add(jlightmap);
        }

        return lightmaps;
    }

    static List<JELightmap> allLightmaps = new List<JELightmap>();

    public string filename;
    public string base64PNG;
    public int base64PNGLength;


}

}

//
