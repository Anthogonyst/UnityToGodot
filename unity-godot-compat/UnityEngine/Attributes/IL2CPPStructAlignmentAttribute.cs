using System;

namespace UnityEngine
{
	[AttributeUsage (AttributeTargets.Struct)]
	internal class IL2CPPStructAlignmentAttribute : Attribute
	{
		//
		// Fields
		//
		public int Align;

		//
		// Constructors
		//
		public IL2CPPStructAlignmentAttribute ()
		{
			this.Align = 1;
		}
	}
}