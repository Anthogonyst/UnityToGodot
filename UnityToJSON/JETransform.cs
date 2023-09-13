// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JETransform : JEComponent
{

    override public void Preprocess()
    {
        unityTransform = unityComponent as Transform;
    }

    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONTransform();
        json.type = "Transform";
        json.localPosition = unityTransform.localPosition;
        json.localRotation = unityTransform.localRotation;
        json.localScale = unityTransform.localScale;
        return json;
    }

    Transform unityTransform;

}

}
