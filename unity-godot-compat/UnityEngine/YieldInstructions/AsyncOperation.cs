using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	[StructLayout (LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		//
		// Fields
		//
		internal IntPtr m_Ptr;

		//
		// Properties
		//
		public bool allowSceneActivation
		{
			get;
			set;
		}

		public bool isDone
		{
			get;
			internal set;
		}

		public int priority
		{
			get;
			set;
		}

		public float progress
		{
			get;
			internal set;
		}

		//
		// Constructors
		//
		~AsyncOperation ()
		{
			this.InternalDestroy ();
		}

		//
		// Methods
		//
		private void InternalDestroy ()
		{
			Debug.Log("Destroy me here");
		}
	}
}