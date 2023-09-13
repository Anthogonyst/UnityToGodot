// Copyright (c) 2014-2015, THUNDERBEAST GAMES LLC
// Licensed under the MIT license, see LICENSE for details

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JSONExporter
{

static class ExportError
{
    public static void FatalError(string message)
    {
        EditorUtility.DisplayDialog("Error", message, "Ok");
        throw new Exception(message);
    }
}


}
