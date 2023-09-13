using System.Collections;
using System.Collections.Generic;
using Godot;

namespace UnityEngine
{
	[UseAsMonoBehaviour]
	public class MonoBehaviour : Node
    {
        List<IEnumerator> coroutines = new List<IEnumerator>();

		public GameObject gameObject { get; private set; }

		public string name { get { return GetName(); } set { SetName(value); } }


		public MonoBehaviour()
		{
			gameObject = new GameObject(this);
		}

		// Probably use an interface as the type contraint
		public static void DontDestroyOnLoad(object obj)
		{
			throw new System.NotImplementedException();
		}

		// Probably use an interface as the type contraint
		public static void Destroy(object obj)
		{
			throw new System.NotImplementedException();
		}
	}
}