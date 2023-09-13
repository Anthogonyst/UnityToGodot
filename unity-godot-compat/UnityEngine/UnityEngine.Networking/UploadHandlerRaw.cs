using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[StructLayout (LayoutKind.Sequential)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		//
		// Constructors
		//
		public UploadHandlerRaw (byte[] data)
		{
			base.InternalCreateRaw (data);
		}

		//
		// Methods
		//
		internal override string GetContentType ()
		{
			return this.InternalGetContentType ();
		}

		internal override byte[] GetData ()
		{
			return this.InternalGetData ();
		}

		internal override float GetProgress ()
		{
			return this.InternalGetProgress ();
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern string InternalGetContentType ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern byte[] InternalGetData ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern float InternalGetProgress ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void InternalSetContentType (string newContentType);

		internal override void SetContentType (string newContentType)
		{
			this.InternalSetContentType (newContentType);
		}
	}
}
