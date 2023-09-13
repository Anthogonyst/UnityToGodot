using Godot;
using System.Collections;

namespace UnityEngine
{
	public abstract class CustomYieldInstruction : Object, IEnumerator
	{
		//
		// Properties
		//
		public object Current {
			get {
				return null;
			}
		}

		public abstract bool keepWaiting {
			get;
		}

		//
		// Methods
		//
		public bool MoveNext ()
		{
			return keepWaiting;
		}

		public void Reset ()
		{
		}
	}
}