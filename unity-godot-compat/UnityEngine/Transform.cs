using System;

namespace UnityEngine
{
	public class Transform
	{
		public Godot.Spatial spatial;

		public string name { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
		public GameObject gameObject { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


		internal Transform(Godot.Spatial node)
		{
			spatial = node;
		}


		public Vector3 position
		{
			get { return spatial.Translation; }
			set { spatial.Translation = value; }
		}


		public void Rotate(Vector3 eulerAngles)
		{
			spatial.Rotate(eulerAngles.normalized, eulerAngles.magnitude * Mathf.Deg2Rad);
		}


		public Vector3 InverseTransformPoint(Vector3 inPoint)
		{
			throw new NotImplementedException();
		}


		public T GetComponent<T>()
		{
			throw new NotImplementedException();
		}


		public T GetComponentInChildren<T>()
		{
			throw new NotImplementedException();
		}


		public T[] GetComponentsInChildren<T>()
		{
			throw new NotImplementedException();
		}
	}
}