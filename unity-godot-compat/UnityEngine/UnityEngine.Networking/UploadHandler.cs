using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[StructLayout (LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		//
		// Fields
		//
		[NonSerialized]
		internal IntPtr m_Ptr;

		//
		// Properties
		//
		public string contentType {
			get {
				return this.GetContentType ();
			}
			set {
				this.SetContentType (value);
			}
		}

		public byte[] data {
			get {
				return this.GetData ();
			}
		}

		public float progress {
			get {
				return this.GetProgress ();
			}
		}

		//
		// Constructors
		//
		internal UploadHandler ()
		{
		}

		~UploadHandler ()
		{
			this.InternalDestroy ();
		}

		//
		// Methods
		//
		public void Dispose ()
		{
			this.InternalDestroy ();
			GC.SuppressFinalize (this);
		}

		internal virtual string GetContentType ()
		{
			return "text/plain";
		}

		internal virtual byte[] GetData ()
		{
			return null;
		}

		internal virtual float GetProgress ()
		{
			return 0.5f;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalCreateRaw (byte[] data);

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void InternalDestroy ();

		internal virtual void SetContentType (string newContentType)
		{
		}
	}
}