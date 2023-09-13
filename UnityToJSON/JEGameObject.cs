// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace JSONExporter
{


public class JEGameObject : JEObject
{
    public JETransform transform;

    public JEGameObject parent;

    public List<JEGameObject> children = new List<JEGameObject>();

    public List<JEComponent> components = new List<JEComponent>();

    public GameObject unityGameObject;

    public static Dictionary<GameObject, JEGameObject> GameObjectLookup;

    public void AddComponent(JEComponent component)
    {
        components.Add(component);
    }

    public JEGameObject(GameObject go, JEGameObject parent)
    {

        GameObjectLookup[go] = this;
        this.unityGameObject = go;

        this.parent = parent;
        this.name = go.name;

        if (parent != null)
            parent.children.Add(this);

        JEComponent.QueryComponents(this);
    }

    // first pass is preprocess
    public void Preprocess()
    {
        foreach (var component in components)
            component.Preprocess();

        foreach (var child in children)
            child.Preprocess();
    }

    // next we query resources
    public void QueryResources()
    {
        foreach (var component in components)
            component.QueryResources();

        foreach (var child in children)
            child.QueryResources();
    }

    // process pass
    public void Process()
    {
        foreach (var component in components)
            component.Process();

        foreach (var child in children)
            child.Process();
    }

    // final pass
    public void PostProcess()
    {
        foreach (var component in components)
            component.PostProcess();

        foreach (var child in children)
            child.PostProcess();
    }

    public static void Reset()
    {
        GameObjectLookup = new Dictionary<GameObject, JEGameObject>();
    }

    public new JSONGameObject ToJSON()
    {
        JSONGameObject json = new JSONGameObject();

        json.name = name;

        json.children = new List<JSONGameObject>();
        foreach (var child in children)
            json.children.Add(child.ToJSON());

        json.components = new List<JSONComponent>();
        foreach (var component in components)
        {
            json.components.Add(component.ToJSON());
        }

        return json;
    }
}

}
