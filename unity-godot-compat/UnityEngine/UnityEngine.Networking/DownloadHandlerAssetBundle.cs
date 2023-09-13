using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	[StructLayout (LayoutKind.Sequential)]
	public sealed class DownloadHandlerAssetBundle : DownloadHandler
	{
		//
		// Properties
		//
		public extern AssetBundle assetBundle {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		//
		// Constructors
		//
		public DownloadHandlerAssetBundle (string url, uint crc)
		{
			base.InternalCreateAssetBundle (url, crc);
		}

		public DownloadHandlerAssetBundle (string url, uint version, uint crc)
		{
			base.InternalCreateAssetBundleCached (url, "", new Hash128 (0u, 0u, 0u, version), crc);
		}

		public DownloadHandlerAssetBundle (string url, Hash128 hash, uint crc)
		{
			base.InternalCreateAssetBundleCached (url, "", hash, crc);
		}

		public DownloadHandlerAssetBundle (string url, string name, Hash128 hash, uint crc)
		{
			base.InternalCreateAssetBundleCached (url, name, hash, crc);
		}

		//
		// Static Methods
		//
		public static AssetBundle GetContent (UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAssetBundle> (www).assetBundle;
		}

		//
		// Methods
		//
		protected override byte[] GetData ()
		{
			throw new NotSupportedException ("Raw data access is not supported for asset bundles");
		}

		protected override string GetText ()
		{
			throw new NotSupportedException ("String access is not supported for asset bundles");
		}
	}
}
