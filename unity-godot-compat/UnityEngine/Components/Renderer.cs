using System;

namespace UnityEngine
{
	public class Renderer : Object
	{
		public Material sharedMaterial;
		public Material material { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
	}
}