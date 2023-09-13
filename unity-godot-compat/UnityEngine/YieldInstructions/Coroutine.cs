using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	[StructLayout (LayoutKind.Sequential)]
	public sealed class Coroutine : CustomYieldInstruction
	{
		IEnumerator routine;


		public Coroutine (IEnumerator routine)
		{
			this.routine = routine;
		}


		public override bool keepWaiting
		{
			get
			{
				return routine.MoveNext();
			}
		}
	}
}
