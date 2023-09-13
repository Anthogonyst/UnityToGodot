using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[StructLayout (LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		//
		// Fields
		//
		[NonSerialized]
		internal IntPtr m_Ptr;

		//
		// Properties
		//
		public byte[] data {
			get {
				return this.GetData ();
			}
		}

		public extern bool isDone {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public string text {
			get {
				return this.GetText ();
			}
		}

		//
		// Constructors
		//
		internal DownloadHandler ()
		{
		}

		~DownloadHandler ()
		{
			this.InternalDestroy ();
		}

		//
		// Static Methods
		//
		protected static T GetCheckedDownloader<T> (UnityWebRequest www) where T : DownloadHandler
		{
			if (www == null) {
				throw new NullReferenceException ("Cannot get content from a null UnityWebRequest object");
			}
			if (!www.isDone) {
				throw new InvalidOperationException ("Cannot get content from an unfinished UnityWebRequest object");
			}
			if (www.isNetworkError) {
				throw new InvalidOperationException (www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_InternalCreateAssetBundleCached (DownloadHandler self, string url, string name, ref Hash128 hash, uint crc);

		//
		// Methods
		//
		[UsedByNativeCode]
		protected virtual void CompleteContent ()
		{
		}

		public void Dispose ()
		{
			this.InternalDestroy ();
			GC.SuppressFinalize (this);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern string GetContentType ();

		protected virtual byte[] GetData ()
		{
			return null;
		}

		[UsedByNativeCode]
		protected virtual float GetProgress ()
		{
			return 0f;
		}

		protected virtual string GetText ()
		{
			byte[] data = this.GetData ();
			string result;
			if (data != null && data.Length > 0) {
				result = this.GetTextEncoder ().GetString (data, 0, data.Length);
			} else {
				result = "";
			}
			return result;
		}

		private Encoding GetTextEncoder ()
		{
			string contentType = this.GetContentType ();
			Encoding result;
			if (!string.IsNullOrEmpty (contentType)) {
				int num = contentType.IndexOf ("charset", StringComparison.OrdinalIgnoreCase);
				if (num > -1) {
					int num2 = contentType.IndexOf ('=', num);
					if (num2 > -1) {
						string text = contentType.Substring (num2 + 1).Trim ().Trim (new char[] {
							'\'',
							'"'
						}).Trim ();
						int num3 = text.IndexOf (';');
						if (num3 > -1) {
							text = text.Substring (0, num3);
						}
						try {
							result = Encoding.GetEncoding (text);
							return result;
						} catch (ArgumentException ex) {
							Debug.LogWarning (string.Format ("Unsupported encoding '{0}': {1}", text, ex.Message));
						}
					}
				}
			}
			result = Encoding.UTF8;
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalCreateAssetBundle (string url, uint crc);

		internal void InternalCreateAssetBundleCached (string url, string name, Hash128 hash, uint crc)
		{
			DownloadHandler.INTERNAL_CALL_InternalCreateAssetBundleCached (this, url, name, ref hash, crc);
		}

		internal void InternalCreateBuffer ()
		{
			Debug.Log("Implement 'InternalCreateBuffer'");
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal extern void InternalCreateScript ();

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void InternalDestroy ();

		[UsedByNativeCode]
		protected virtual void ReceiveContentLength (int contentLength)
		{
		}

		[UsedByNativeCode]
		protected virtual bool ReceiveData (byte[] data, int dataLength)
		{
			return true;
		}
	}
}
