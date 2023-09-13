// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JSONTexture
{
    public string name;
    public string base64PNG;
    public int base64PNGLength;
}

public class JSONShader
{
    public string name;
    public int renderQueue;
}

public class JSONMaterial
{
    public string name;
    public string shader;
    public string mainTexture;
    public Vector2 mainTextureOffset;
    public Vector2 mainTextureScale;
    public int passCount;
    public int renderQueue;
    public string[] shaderKeywords;
    public Color color;

}

public class JSONBoneWeight
{
    public int[] indexes = new int[4];
    public float[] weights = new float[4];
}

public class JSONMesh
{
    public string name;
    public int subMeshCount;
    public int[][] triangles;
    public int vertexCount;
    public Vector3[] vertexPositions;
    public Vector2[] vertexUV;
    public Vector2[] vertexUV2;
    public Color[] vertexColors;
    public Vector3[] vertexNormals;
    public Vector4[] vertexTangents;
    public Matrix4x4[] bindPoses;
    public JSONBoneWeight[] boneWeights;
    public JSONTransform[] bones;
    public string rootBone;

}

public class JSONLightmap
{
    public string filename;
    public string base64PNG;
    public int base64PNGLength;
}

public class JSONGameObject
{
    public string name;
    public List<JSONComponent> components;
    public List<JSONGameObject> children;

    public T GetComponent<T>() where T: JSONComponent
    {
        foreach(var component in components)
            if (component.GetType() == typeof(T))
                return (T) component;

        return null;
    }

}

public class JSONComponent
{
    public string type;
}

public class JSONTransform : JSONComponent
{
    public Vector3 localPosition;
    public Quaternion localRotation;
    public Vector3 localScale;
    public String name;
    public String parentName;
}

public class JSONTimeOfDay : JSONComponent
{
    public float timeOn;
    public float timeOff;
}

public class JSONCamera : JSONComponent
{

}

public class JSONLight : JSONComponent
{
    public Color color;
    public float range;
    public string lightType;
    public bool castsShadows;
    public bool realtime;
}


public class JSONBoxCollider : JSONComponent
{
    public Vector3 center;
    public Vector3 size;
}

public class JSONMeshCollider : JSONComponent
{
}

public class JSONRigidBody : JSONComponent
{
    public float mass;
}

public class JSONMeshRenderer : JSONComponent
{
    public string mesh;
    public bool enabled;
    public bool castShadows;
    public bool receiveShadows;
    public int lightmapIndex;
    public Vector4 lightmapTilingOffset;
    public string[] materials;
}

public class JSONSkinnedMeshRenderer : JSONComponent
{
    public string mesh;
    public string rootBone;
    public bool enabled;
    public bool castShadows;
    public bool receiveShadows;
    public string[] materials;
}

public class JSONKeyframe
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
    public float time;
}

public class JSONAnimationNode
{
    public string name;
    public JSONKeyframe[] keyframes;
}


public class JSONAnimationClip
{
    public string name;
    public JSONAnimationNode[] nodes;
}

public class JSONAnimation : JSONComponent
{
    public JSONAnimationClip[] clips;
}


public class JSONTerrain : JSONComponent
{
    public int heightmapHeight;
    public int heightmapWidth;
    public int heightmapResolution;

    public Vector3 heightmapScale;
    public Vector3 size;

    public int alphamapWidth;
    public int alphamapHeight;
    public int alphamapLayers;

    public string base64Height;
    public int base64HeightLength;

    public string base64Alpha;
    public int base64AlphaLength;

}


public class JSONResources
{
    public List<JSONTexture> textures;
    public List<JSONLightmap> lightmaps;
    public List<JSONShader> shaders;
    public List<JSONMaterial> materials;
    public List<JSONMesh> meshes;

    public JSONMaterial GetMaterial(string name)
    {
        foreach (var material in materials)
            if (material.name == name)
                return material;

        return null;
    }

    public JSONMesh GetMesh(string name)
    {
        foreach (var mesh in meshes)
            if (mesh.name == name)
                return mesh;

        return null;
    }
}

public class JSONScene
{
    public string name;
    public JSONResources resources;
    public List<JSONGameObject> hierarchy;
}

public class BasicTypeConverter : JsonConverter
 {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value != null)
        {
            if (value.GetType() == typeof(Color))
            {
                Color color = (Color) value;
                writer.WriteStartArray();
                writer.WriteValue(color.r);
                writer.WriteValue(color.g);
                writer.WriteValue(color.b);
                writer.WriteValue(color.a);
                writer.WriteEndArray();
            }
            else if (value.GetType() == typeof(Vector2))
            {
                Vector2 v = (Vector2) value;
                writer.WriteStartArray();
                writer.WriteValue(v.x);
                writer.WriteValue(v.y);
                writer.WriteEndArray();
            }
            else if (value.GetType() == typeof(Vector3))
            {
                Vector3 v = (Vector3) value;
                writer.WriteStartArray();
                writer.WriteValue(v.x);
                writer.WriteValue(v.y);
                writer.WriteValue(v.z);
                writer.WriteEndArray();
            }
            else if (value.GetType() == typeof(Vector4))
            {
                Vector4 v = (Vector4) value;
                writer.WriteStartArray();
                writer.WriteValue(v.x);
                writer.WriteValue(v.y);
                writer.WriteValue(v.z);
                writer.WriteValue(v.w);
                writer.WriteEndArray();
            }
            else if (value.GetType() == typeof(Quaternion))
            {
                Quaternion q = (Quaternion) value;
                writer.WriteStartArray();
                writer.WriteValue(q.x);
                writer.WriteValue(q.y);
                writer.WriteValue(q.z);
                writer.WriteValue(q.w);
                writer.WriteEndArray();
            }
            else if (value.GetType() == typeof(Matrix4x4))
            {
                Matrix4x4 m = (Matrix4x4) value;
                writer.WriteStartArray();

                for (int y = 0; y < 4; y++)
                    for (int x = 0; x < 4; x++)
                        writer.WriteValue(m[y,x]);

                writer.WriteEndArray();
            }

        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return null;
    }

    public override bool CanRead
    {
        get { return true; }
    }

    public override bool CanConvert(Type objectType)
    {
        Type[] types = new Type[]{typeof(Color), typeof(Vector2), typeof(Vector3), typeof(Vector4), typeof(Quaternion), typeof(Matrix4x4)};
        return Array.IndexOf(types, objectType) != -1;
    }
}

}
