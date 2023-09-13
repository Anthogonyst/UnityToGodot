using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	public static class ImageConversion
	{
		//
		// Static Methods
		//
		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToEXR (this Texture2D tex, [DefaultValue ("Texture2D.EXRFlags.None")] Texture2D.EXRFlags flags);

		[ExcludeFromDocs]
		public static byte[] EncodeToEXR (this Texture2D tex)
		{
			Texture2D.EXRFlags flags = Texture2D.EXRFlags.None;
			return tex.EncodeToEXR (flags);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToJPG (this Texture2D tex, int quality);

		public static byte[] EncodeToJPG (this Texture2D tex)
		{
			return tex.EncodeToJPG (75);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern byte[] EncodeToPNG (this Texture2D tex);


		public static bool LoadImage (this Texture2D tex, byte[] data, [DefaultValue ("false")] bool markNonReadable)
		{
			try
			{
				tex.LoadRawTextureData(data);
				return true;
			}
			catch
			{
				return false;
			}
		}


		public static bool LoadImage (this Texture2D tex, byte[] data)
		{
			bool markNonReadable = false;
			return tex.LoadImage (data, markNonReadable);
		}
	}
}
