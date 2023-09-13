using System;
using System.IO;
using System.Text;

namespace Godot
{
    static class Util
    {
        public static StreamWriter CreateStreamWriter(string path)
        {
            // Godot expects files to be UTF-8, without BOM, and Unix line endings
            StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(false));
            sw.NewLine = "\n";
            return sw;
        }

        public static T Cast<T>(object obj)
        {
            if(obj is T)
            {
                return (T)obj;
            }

            throw new InvalidCastException("Tried to cast from " + obj.GetType() + " to " + typeof(T));
        }
    }
}
