// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JEShader : JEResource
{
    private JEShader(Shader shader)
    {
        this.unityShader = shader;
        this.name = shader.name;
        allShaders[shader] = this;
    }

    public static JEShader RegisterShader(Shader shader)
    {
        if (allShaders.ContainsKey(shader))
            return allShaders[shader];

        return new JEShader(shader);
    }

    void preprocess()
    {
        //Debug.Log("preprocess - " + unityShader);
    }

    void process()
    {
        //Debug.Log("process - " + unityShader);
    }

    void postprocess()
    {
        //Debug.Log("postprocess - " + unityShader);
    }

    new public static void Preprocess()
    {
        foreach (var shader in allShaders.Values)
        {
            shader.preprocess();
        }
    }

    new public static void Process()
    {
        foreach (var shader in allShaders.Values)
        {
            shader.process();
        }
    }

    new public static void PostProcess()
    {
        foreach (var shader in allShaders.Values)
        {
            shader.postprocess();
        }
    }

    new public static void Reset()
    {
        allShaders = new Dictionary<Shader, JEShader >();
    }

    public new JSONShader ToJSON()
    {
        var json = new JSONShader();

        json.name = name;
        json.renderQueue = unityShader.renderQueue;

        return json;
    }

    public static List<JSONShader> GenerateJSONShaderList()
    {
        List<JSONShader> shaders = new List<JSONShader>();

        foreach (var shader in allShaders.Values)
            shaders.Add(shader.ToJSON());

        return shaders;
    }

    Shader unityShader;

    public static Dictionary<Shader, JEShader> allShaders;

}


}
