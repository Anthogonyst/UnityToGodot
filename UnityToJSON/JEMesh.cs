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

public class JEMesh : JEResource
{
    public Mesh unityMesh;
    public int index;

    public BoneWeight[] boneWeights;

    // set by skinned renderer if any
    public Transform[] bones;
    public String rootBone = "";

    private JEMesh(Mesh mesh)
    {
        unityMesh = mesh;
        index = allMeshes.Count;
        allMeshes[mesh] = this;

        // we can have duplicates with same name, referenced by index
        //#name = mesh.name;

        string path = AssetDatabase.GetAssetPath(mesh);
        path = Path.GetFileNameWithoutExtension(path);
        name = path + "_" + mesh.name;
    }

    void preprocess()
    {

    }

    void process()
    {

    }

    void postprocess()
    {

    }

    new public static void Preprocess()
    {
        foreach (var mesh in allMeshes.Values)
        {
            mesh.preprocess();
        }
    }

    new public static void Process()
    {
        foreach (var mesh in allMeshes.Values)
        {
            mesh.process();
        }
    }

    new public static void PostProcess()
    {
        foreach (var mesh in allMeshes.Values)
        {
            mesh.postprocess();
        }
    }

    public static JEMesh RegisterMesh(Mesh mesh)
    {
        if (allMeshes.ContainsKey(mesh))
        {
            return allMeshes[mesh];
        }

        return new JEMesh(mesh);
    }

    new public static void Reset()
    {

    }

    public new JSONMesh ToJSON()
    {
        var json = new JSONMesh();
        json.name = name;

        // submeshes
        json.subMeshCount = unityMesh.subMeshCount;
        json.triangles = new int[unityMesh.subMeshCount][];
        for (int i = 0; i < unityMesh.subMeshCount;i++)
        {
            json.triangles[i] = unityMesh.GetTriangles(i);
        }

        // Vertices
        json.vertexCount = unityMesh.vertexCount;
        json.vertexPositions = unityMesh.vertices;
        json.vertexUV = unityMesh.uv;
        json.vertexUV2 = unityMesh.uv2;
        json.vertexColors = unityMesh.colors;
        json.vertexNormals = unityMesh.normals;
        json.vertexTangents = unityMesh.tangents;
        json.bindPoses = unityMesh.bindposes;
        json.boneWeights = new JSONBoneWeight[unityMesh.boneWeights.Length];

        json.rootBone = rootBone;

        for (int i = 0; i < unityMesh.boneWeights.Length; i++)
        {
            var bw = unityMesh.boneWeights[i];
            var jbw = new JSONBoneWeight();
            jbw.indexes[0] = bw.boneIndex0;
            jbw.indexes[1] = bw.boneIndex1;
            jbw.indexes[2] = bw.boneIndex2;
            jbw.indexes[3] = bw.boneIndex3;
            jbw.weights[0] = bw.weight0;
            jbw.weights[1] = bw.weight1;
            jbw.weights[2] = bw.weight2;
            jbw.weights[3] = bw.weight3;
            json.boneWeights[i] = jbw;
        }

        if (bones != null)
        {
            JSONTransform[] jbones = new JSONTransform[bones.Length];
            for (int i = 0; i < bones.Length; i++)
            {
                JSONTransform jt = new JSONTransform();
                jt.localPosition = bones[i].localPosition;
                jt.localRotation = bones[i].localRotation;
                jt.localScale = bones[i].localScale;
                jt.name = bones[i].gameObject.name;
                jt.parentName = bones[i].parent == null ? "" : bones[i].parent.gameObject.name;
                jt.type = "Bone";
                jbones[i] = jt;
            }
            json.bones = jbones;

        }

        return json;
    }

    public static List<JSONMesh> GenerateJSONMeshList()
    {
        List<JSONMesh> meshes = new List<JSONMesh>();

        foreach (var mesh in allMeshes.Values)
            meshes.Add(mesh.ToJSON());

        return meshes;
    }

    public static Dictionary<Mesh, JEMesh> allMeshes = new Dictionary<Mesh, JEMesh >();

}

}
