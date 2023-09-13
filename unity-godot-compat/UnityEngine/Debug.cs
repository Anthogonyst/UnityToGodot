using Godot;

namespace UnityEngine
{
	public static class Debug
	{
		const string nullString = "Null";

		public static bool isDebugBuild { get { return OS.IsDebugBuild(); } }


		public static void Log(object obj)
		{
			if ( obj != null )
				GD.Print(obj.ToString());
			else
				GD.Print(nullString);
		}


		public static void LogWarning(object obj)
		{
			if ( obj != null )
				GD.Print(obj.ToString());
			else
				GD.Print(nullString);
		}


		public static void LogError(object obj)
		{
			if ( obj != null )
				GD.PrintErr(obj.ToString());
			else
				GD.Print(nullString);
		}
	}
}