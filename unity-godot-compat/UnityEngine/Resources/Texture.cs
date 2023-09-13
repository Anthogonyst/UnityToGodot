using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	public class Texture : UnityEngine.Object
	{
		//
		// Static Properties
		//
		public static extern AnisotropicFiltering anisotropicFiltering {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int masterTextureLimit {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		//
		// Properties
		//
		public extern int anisoLevel {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public virtual TextureDimension dimension {
			get {
				return Texture.Internal_GetDimension (this);
			}
			set {
				throw new Exception ("not implemented");
			}
		}

		public extern FilterMode filterMode {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public virtual int height {
			get {
				return Texture.Internal_GetHeight (this);
			}
			set {
				throw new Exception ("not implemented");
			}
		}

		public extern float mipMapBias {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 texelSize {
			get {
				Vector2 result;
				this.INTERNAL_get_texelSize (out result);
				return result;
			}
		}

		public virtual int width {
			get {
				return Texture.Internal_GetWidth (this);
			}
			set {
				throw new Exception ("not implemented");
			}
		}

		public extern TextureWrapMode wrapMode {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeU {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeV {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeW {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		//
		// Static Methods
		//
		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetNativeTexturePtr (Texture self, out IntPtr value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern TextureDimension Internal_GetDimension (Texture t);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHeight (Texture t);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern int Internal_GetWidth (Texture t);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern void SetGlobalAnisotropicFilteringLimits (int forcedMin, int globalMax);

		//
		// Methods
		//
		[Obsolete ("Use GetNativeTexturePtr instead."), GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern int GetNativeTextureID ();

		public IntPtr GetNativeTexturePtr ()
		{
			IntPtr result;
			Texture.INTERNAL_CALL_GetNativeTexturePtr (this, out result);
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_texelSize (out Vector2 value);
	}
}
