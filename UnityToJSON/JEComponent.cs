// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JEComponent : JEObject
{
    public Component unityComponent;
    public JEGameObject jeGameObject;

    virtual public void QueryResources()
    {

    }

    virtual public void Preprocess()
    {

    }

    virtual public void Process()
    {

    }

    virtual public void PostProcess()
    {

    }

    public virtual new JSONComponent ToJSON()
    {
        throw new NotImplementedException("Attempting to call JEComponent ToJSON (override method)");
    }

    public static void Reset()
    {
        JETransform.Reset();
        JEMeshRenderer.Reset();
        conversions = new Dictionary<Type, Type >();
    }

    public static void QueryComponents(JEGameObject jgo)
    {
        // for every registered conversion get that component
        foreach( KeyValuePair<Type,Type> pair in conversions)
        {
            Component[] components = jgo.unityGameObject.GetComponents(pair.Key);

            foreach (Component component in components)
            {

                MeshRenderer meshRenderer = component as MeshRenderer;
                if (meshRenderer != null && !meshRenderer.enabled)
                    continue;

                var jcomponent = Activator.CreateInstance(pair.Value) as JEComponent;

                if (jcomponent == null)
                {
                    ExportError.FatalError("Export component creation failed");
                }

                jcomponent.unityComponent = component;
                jcomponent.jeGameObject = jgo;
                jgo.AddComponent(jcomponent);

            }
        }
    }


    public static void RegisterConversion(Type componentType, Type exportType)
    {
        conversions[componentType] = exportType;
    }

    public static void RegisterStandardComponents()
    {
        RegisterConversion(typeof(Transform), typeof(JETransform));
        RegisterConversion(typeof(Camera), typeof(JECamera));
        RegisterConversion(typeof(MeshRenderer), typeof(JEMeshRenderer));
        RegisterConversion(typeof(SkinnedMeshRenderer), typeof(JESkinnedMeshRenderer));
        RegisterConversion(typeof(Terrain), typeof(JETerrain));
        RegisterConversion(typeof(JSONAnimationHelper), typeof(JEAnimation));
        RegisterConversion(typeof(BoxCollider), typeof(JEBoxCollider));
        RegisterConversion(typeof(MeshCollider), typeof(JEMeshCollider));
        RegisterConversion(typeof(Rigidbody), typeof(JERigidBody));
        RegisterConversion(typeof(Light), typeof(JELight));
        RegisterConversion(typeof(TimeOfDay), typeof(JETimeOfDay));
    }

    static Dictionary<Type, Type> conversions;

}

}
