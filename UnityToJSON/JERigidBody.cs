// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JERigidBody : JEComponent
{

    override public void Preprocess()
    {
        unityRigidBody = unityComponent as Rigidbody;
    }

    override public void QueryResources()
    {
    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONRigidBody();
        json.type = "RigidBody";
        json.mass = unityRigidBody.mass;
        return json;
    }

    Rigidbody unityRigidBody;
}

}
