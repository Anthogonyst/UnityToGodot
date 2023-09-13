using System;

namespace UnityEngine.Scripting
{
	[AttributeUsage (AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct, Inherited = false)]
	internal class RequiredByNativeCodeAttribute : Attribute
	{
		//
		// Properties
		//
		public string Name {
			get;
			set;
		}

		public bool Optional {
			get;
			set;
		}

		//
		// Constructors
		//
		public RequiredByNativeCodeAttribute ()
		{
		}

		public RequiredByNativeCodeAttribute (string name)
		{
			this.Name = name;
		}

		public RequiredByNativeCodeAttribute (bool optional)
		{
			this.Optional = optional;
		}

		public RequiredByNativeCodeAttribute (string name, bool optional)
		{
			this.Name = name;
			this.Optional = optional;
		}
	}
}
