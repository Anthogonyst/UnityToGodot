using System;

namespace UnityEngine.Scripting
{
	[AttributeUsage (AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, Inherited = false)]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		//
		// Properties
		//
		public string Name {
			get;
			set;
		}

		//
		// Constructors
		//
		public UsedByNativeCodeAttribute ()
		{
		}

		public UsedByNativeCodeAttribute (string name)
		{
			this.Name = name;
		}
	}
}