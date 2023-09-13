// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEditor;


namespace JSONExporter
{

public class JEObject
{
    public string name;

    public object ToJSON()
    {
        throw new NotImplementedException("Attempting to call JEObject ToJSON (override method)");
    }

}

}
