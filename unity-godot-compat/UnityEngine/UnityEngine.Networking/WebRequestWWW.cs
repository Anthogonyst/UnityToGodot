using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	internal static class WebRequestWWW
	{
		//
		// Static Methods
		//
		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal static extern AudioClip InternalCreateAudioClipUsingDH (DownloadHandler dh, string url, bool stream, bool compressed, AudioType audioType);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		internal static extern MovieTexture InternalCreateMovieTextureUsingDH (DownloadHandler dh);
	}
}
