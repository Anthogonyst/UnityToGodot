using System;

namespace UnityEngine
{
	public static class Input
	{
		public static Vector3 mousePosition { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


		public static bool GetKey(KeyCode key)
		{
			Debug.Log("Input.GetKey not implemented");
			return false;
		}


		public static bool GetKeyUp(KeyCode key)
		{
			Debug.Log("Input.GetKeyUp not implemented");
			return false;
		}


		public static bool GetKeyDown(KeyCode key)
		{
			Debug.Log("Input.GetKeyDown not implemented");
			return false;
		}
	}
}
