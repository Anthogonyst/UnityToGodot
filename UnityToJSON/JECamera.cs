// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

public class JECamera : JEComponent
{
    new public static void Reset()
    {

    }

    public override JSONComponent ToJSON()
    {
        var json = new JSONCamera();
        json.type = "Camera";
        return json;
    }

}

}
