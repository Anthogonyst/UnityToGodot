using System.Collections.Generic;

namespace Godot
{
    interface IData
    {
        Dictionary<string, object> GetData();
    }

    // Class names must match exactly

    class Object : IData
    {
        public Script script;
        public Dictionary<string, object> scriptVariables;

        public virtual Dictionary<string, object> GetData()
        {
            var d = new Dictionary<string, object>();
            if(script != null)
            {
                d["script"] = script;
            }
            if(scriptVariables != null)
            {
                foreach(var kv in scriptVariables)
                {
                    d[kv.Key] = kv.Value;
                }
            }
            return d;
        }
    }

    class Reference : Godot.Object
    { }
}
