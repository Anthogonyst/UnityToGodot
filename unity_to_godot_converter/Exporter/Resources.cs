using System.Collections.Generic;
using UnityEngine;

namespace Godot
{
    // Class names must match exactly

    class Resource : Reference
    {
        public string resourcePath;
    }

    class Script : Resource
    { }

    class Texture : Resource
    { }

    class AtlasTexture : Texture
    {
        public Texture atlas;
        public Rect region;
        public Rect margin = new Rect();

        public override Dictionary<string, object> GetData()
        {
            var d = base.GetData();
            d["atlas"] = atlas;
            d["region"] = region;
            d["margin"] = margin;
            return d;
        }
    }
}

