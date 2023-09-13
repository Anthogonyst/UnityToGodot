using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	public sealed class AudioClip : Object
	{
		public delegate void PCMReaderCallback (float[] data);

		public delegate void PCMSetPositionCallback (int position);

		//
		// Properties
		//
		public bool ambisonic
		{
			get;
		}

		public int channels
		{
			get;
		}

		public int frequency
		{
			get;
		}

		public float length
		{
			get;
		}

		public bool loadInBackground
		{
			get;
		}

		public AudioDataLoadState loadState
		{
			get;
		}

		public AudioClipLoadType loadType
		{
			get;
		}

		public bool preloadAudioData
		{
			get;
		}

		public int samples
		{
			get;
		}

/*		//
		// Constructors
		//
		public AudioClip ()
		{
			//this.m_PCMReaderCallback = null;
			//this.m_PCMSetPositionCallback = null;
			//base..ctor ();
		}

		//
		// Static Methods
		//
		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern AudioClip Construct_Internal ();

		[Obsolete ("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create (name, lengthSamples, channels, frequency, stream);
		}

		[Obsolete ("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create (name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		[Obsolete ("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create (name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create (name, lengthSamples, channels, frequency, stream, null, null);
		}

		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create (name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		public static AudioClip Create (string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null) {
				throw new NullReferenceException ();
			}
			if (lengthSamples <= 0) {
				throw new ArgumentException ("Length of created clip must be larger than 0");
			}
			if (channels <= 0) {
				throw new ArgumentException ("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0) {
				throw new ArgumentException ("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = AudioClip.Construct_Internal ();
			if (pcmreadercallback != null) {
				audioClip.m_PCMReaderCallback += pcmreadercallback;
			}
			if (pcmsetpositioncallback != null) {
				audioClip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			audioClip.Init_Internal (name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		//
		// Methods
		//
		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern bool GetData (float[] data, int offsetSamples);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void Init_Internal (string name, int lengthSamples, int channels, int frequency, bool stream);

		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal (float[] data)
		{
			if (this.m_PCMReaderCallback != null) {
				this.m_PCMReaderCallback (data);
			}
		}

		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal (int position)
		{
			if (this.m_PCMSetPositionCallback != null) {
				this.m_PCMSetPositionCallback (position);
			}
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern bool LoadAudioData ();

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern bool SetData (float[] data, int offsetSamples);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern bool UnloadAudioData ();

		//
		// Events
		//
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback {
			add {
				AudioClip.PCMReaderCallback pCMReaderCallback = this.m_PCMReaderCallback;
				AudioClip.PCMReaderCallback pCMReaderCallback2;
				do {
					pCMReaderCallback2 = pCMReaderCallback;
					pCMReaderCallback = Interlocked.CompareExchange<AudioClip.PCMReaderCallback> (ref this.m_PCMReaderCallback, (AudioClip.PCMReaderCallback)Delegate.Combine (pCMReaderCallback2, value), pCMReaderCallback);
				} while (pCMReaderCallback != pCMReaderCallback2);
			}
			remove {
				AudioClip.PCMReaderCallback pCMReaderCallback = this.m_PCMReaderCallback;
				AudioClip.PCMReaderCallback pCMReaderCallback2;
				do {
					pCMReaderCallback2 = pCMReaderCallback;
					pCMReaderCallback = Interlocked.CompareExchange<AudioClip.PCMReaderCallback> (ref this.m_PCMReaderCallback, (AudioClip.PCMReaderCallback)Delegate.Remove (pCMReaderCallback2, value), pCMReaderCallback);
				} while (pCMReaderCallback != pCMReaderCallback2);
			}
		}

		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback {
			add {
				AudioClip.PCMSetPositionCallback pCMSetPositionCallback = this.m_PCMSetPositionCallback;
				AudioClip.PCMSetPositionCallback pCMSetPositionCallback2;
				do {
					pCMSetPositionCallback2 = pCMSetPositionCallback;
					pCMSetPositionCallback = Interlocked.CompareExchange<AudioClip.PCMSetPositionCallback> (ref this.m_PCMSetPositionCallback, (AudioClip.PCMSetPositionCallback)Delegate.Combine (pCMSetPositionCallback2, value), pCMSetPositionCallback);
				} while (pCMSetPositionCallback != pCMSetPositionCallback2);
			}
			remove {
				AudioClip.PCMSetPositionCallback pCMSetPositionCallback = this.m_PCMSetPositionCallback;
				AudioClip.PCMSetPositionCallback pCMSetPositionCallback2;
				do {
					pCMSetPositionCallback2 = pCMSetPositionCallback;
					pCMSetPositionCallback = Interlocked.CompareExchange<AudioClip.PCMSetPositionCallback> (ref this.m_PCMSetPositionCallback, (AudioClip.PCMSetPositionCallback)Delegate.Remove (pCMSetPositionCallback2, value), pCMSetPositionCallback);
				} while (pCMSetPositionCallback != pCMSetPositionCallback2);
			}
		}*/
	}
}