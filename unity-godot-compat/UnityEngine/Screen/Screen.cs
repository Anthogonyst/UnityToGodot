using System;
namespace UnityEngine
{
	public static class Screen
	{
		public static int width;
		public static int height;

		public static bool fullScreen
		{
			get
			{
				Debug.Log("Implement: fullScreen");
				return false;
			}
		}


		public static Resolution currentResolution
		{
			get
			{
				Debug.Log("Implement: currentResolution");
				return null;
			}
		}


		public static void SetResolution(int width, int height, bool fullScreen)
		{
			Debug.Log("Implement: SetResolution");
		}
	}
}
