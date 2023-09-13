using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	public class Texture2D : Texture
	{
		Godot.ImageTexture _texture = null;

		//
		// Static Properties
		//
		public static extern Texture2D blackTexture {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public static extern Texture2D whiteTexture {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		//
		// Properties
		//
		public extern bool alphaIsTransparency {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureFormat format {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		public extern int mipmapCount {
			[GeneratedByOldBindingsGenerator]
			[MethodImpl (MethodImplOptions.InternalCall)]
			get;
		}

		//
		// Constructors
		//
		public Texture2D (int width, int height)
		{
			Internal_Create (this, width, height, TextureFormat.RGBA32, true, false, IntPtr.Zero);
		}

		public Texture2D (int width, int height, TextureFormat format, bool mipmap)
		{
			Internal_Create (this, width, height, format, mipmap, false, IntPtr.Zero);
		}

		public Texture2D (int width, int height, TextureFormat format, bool mipmap, bool linear)
		{
			Internal_Create (this, width, height, format, mipmap, linear, IntPtr.Zero);
		}

		internal Texture2D (int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			Internal_Create (this, width, height, format, mipmap, linear, nativeTex);
		}

		internal Texture2D (byte[] data)
		{
			_texture = new Godot.ImageTexture();
			LoadRawTextureData(data);
		}

		private Texture2D(Godot.ImageTexture texture)
		{
			_texture = texture;
		}

		//
		// Conversion Methods
		//
		public static implicit operator Godot.ImageTexture (Texture2D texture2D)
		{
			return texture2D._texture;
		}


		public static implicit operator Texture2D (Godot.ImageTexture godotImageTexture)
		{
			return new Texture2D(godotImageTexture);
		}

		//
		// Static Methods
		//
		public static Texture2D CreateExternalTexture (int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			if (nativeTex == IntPtr.Zero) {
				throw new ArgumentException ("nativeTex can not be null");
			}
			return new Texture2D (width, height, format, mipmap, linear, nativeTex);
		}

		public static bool GenerateAtlas (Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			if (sizes == null) {
				throw new ArgumentException ("sizes array can not be null");
			}
			if (results == null) {
				throw new ArgumentException ("results list cannot be null");
			}
			if (padding < 0) {
				throw new ArgumentException ("padding can not be negative");
			}
			if (atlasSize <= 0) {
				throw new ArgumentException ("atlas size must be positive");
			}
			results.Clear ();
			bool result;
			if (sizes.Length == 0) {
				result = true;
			} else {
				Texture2D.GenerateAtlasInternal (sizes, padding, atlasSize, results);
				result = (results.Count != 0);
			}
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void GenerateAtlasInternal (Vector2[] sizes, int padding, int atlasSize, object resultList);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Compress (Texture2D self, bool highQuality);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixel (Texture2D self, int x, int y, out Color value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetPixelBilinear (Texture2D self, float u, float v, out Color value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ReadPixels (Texture2D self, ref Rect source, int destX, int destY, bool recalculateMipMaps);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPixel (Texture2D self, int x, int y, ref Color color);

		private static void Internal_Create (Texture2D mono, int width, int height, TextureFormat format, bool mipmap, bool linear, IntPtr nativeTex)
		{
			mono._texture = new Godot.ImageTexture();
		}

		//
		// Methods
		//
		[ExcludeFromDocs]
		public void Apply (bool updateMipmaps)
		{
			bool makeNoLongerReadable = false;
			this.Apply (updateMipmaps, makeNoLongerReadable);
		}

		[ExcludeFromDocs]
		public void Apply ()
		{
			bool makeNoLongerReadable = false;
			bool updateMipmaps = true;
			this.Apply (updateMipmaps, makeNoLongerReadable);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern void Apply ([DefaultValue ("true")] bool updateMipmaps, [DefaultValue ("false")] bool makeNoLongerReadable);

		public void Compress (bool highQuality)
		{
			Texture2D.INTERNAL_CALL_Compress (this, highQuality);
		}

		public Color GetPixel (int x, int y)
		{
			Color result;
			Texture2D.INTERNAL_CALL_GetPixel (this, x, y, out result);
			return result;
		}

		public Color GetPixelBilinear (float u, float v)
		{
			Color result;
			Texture2D.INTERNAL_CALL_GetPixelBilinear (this, u, v, out result);
			return result;
		}

		[ExcludeFromDocs]
		public Color[] GetPixels ()
		{
			int miplevel = 0;
			return this.GetPixels (miplevel);
		}

		[ExcludeFromDocs]
		public Color[] GetPixels (int x, int y, int blockWidth, int blockHeight)
		{
			int miplevel = 0;
			return this.GetPixels (x, y, blockWidth, blockHeight, miplevel);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern Color[] GetPixels (int x, int y, int blockWidth, int blockHeight, [DefaultValue ("0")] int miplevel);

		public Color[] GetPixels ([DefaultValue ("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1) {
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1) {
				num2 = 1;
			}
			return this.GetPixels (0, 0, num, num2, miplevel);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern Color32[] GetPixels32 ([DefaultValue ("0")] int miplevel);

		[ExcludeFromDocs]
		public Color32[] GetPixels32 ()
		{
			int miplevel = 0;
			return this.GetPixels32 (miplevel);
		}

		public byte[] GetRawTextureData ()
		{
			return _texture.GetData().GetData();
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern bool Internal_ResizeWH (int width, int height);

		public void LoadRawTextureData (byte[] data)
		{
			Godot.Image image = new Godot.Image();
			Bitmap bm;

			try
			{
				using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
				{
					bm = new Bitmap(ms);
				}

				byte[] bytes = new byte[bm.Width * bm.Height * 4];
				int bytePos = 0;

				for ( int y = 0; y < bm.Height; y++)
				{
					for ( int x = 0; x < bm.Width; x++)
					{
						var color = bm.GetPixel(x, y);
						bytes[bytePos] = color.R;
						bytes[bytePos+1] = color.G;
						bytes[bytePos+2] = color.B;
						bytes[bytePos+3] = color.A;
						bytePos += 4;
					}
				}

				image.CreateFromData(bm.Width, bm.Height, false, Godot.Image.Format.Rgba8, bytes);
				_texture.CreateFromImage(image);
			}
			catch (System.Exception e)
			{
				Debug.Log(e.Message + "\n\n" + e.StackTrace);
			}
		}

		public void LoadRawTextureData (IntPtr data, int size)
		{
			this.LoadRawTextureData_ImplPointer (data, size);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void LoadRawTextureData_ImplPointer (IntPtr data, int size);

		[ExcludeFromDocs]
		public Rect[] PackTextures (Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			bool makeNoLongerReadable = false;
			return this.PackTextures (textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern Rect[] PackTextures (Texture2D[] textures, int padding, [DefaultValue ("2048")] int maximumAtlasSize, [DefaultValue ("false")] bool makeNoLongerReadable);

		[ExcludeFromDocs]
		public Rect[] PackTextures (Texture2D[] textures, int padding)
		{
			bool makeNoLongerReadable = false;
			int maximumAtlasSize = 2048;
			return this.PackTextures (textures, padding, maximumAtlasSize, makeNoLongerReadable);
		}

		[ExcludeFromDocs]
		public void ReadPixels (Rect source, int destX, int destY)
		{
			bool recalculateMipMaps = true;
			Texture2D.INTERNAL_CALL_ReadPixels (this, ref source, destX, destY, recalculateMipMaps);
		}

		public void ReadPixels (Rect source, int destX, int destY, [DefaultValue ("true")] bool recalculateMipMaps)
		{
			Texture2D.INTERNAL_CALL_ReadPixels (this, ref source, destX, destY, recalculateMipMaps);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern bool Resize (int width, int height, TextureFormat format, bool hasMipMap);

		public bool Resize (int width, int height)
		{
			return this.Internal_ResizeWH (width, height);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void SetAllPixels32 (Color32[] colors, int miplevel);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private extern void SetBlockOfPixels32 (int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		public void SetPixel (int x, int y, Color color)
		{
			Texture2D.INTERNAL_CALL_SetPixel (this, x, y, ref color);
		}

		[ExcludeFromDocs]
		public void SetPixels (int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels (x, y, blockWidth, blockHeight, colors, miplevel);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern void SetPixels (int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue ("0")] int miplevel);

		public void SetPixels (Color[] colors, [DefaultValue ("0")] int miplevel)
		{
			int num = this.width >> miplevel;
			if (num < 1) {
				num = 1;
			}
			int num2 = this.height >> miplevel;
			if (num2 < 1) {
				num2 = 1;
			}
			this.SetPixels (0, 0, num, num2, colors, miplevel);
		}

		[ExcludeFromDocs]
		public void SetPixels (Color[] colors)
		{
			int miplevel = 0;
			this.SetPixels (colors, miplevel);
		}

		[ExcludeFromDocs]
		public void SetPixels32 (int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			int miplevel = 0;
			this.SetPixels32 (x, y, blockWidth, blockHeight, colors, miplevel);
		}

		public void SetPixels32 (Color32[] colors, [DefaultValue ("0")] int miplevel)
		{
			this.SetAllPixels32 (colors, miplevel);
		}

		[ExcludeFromDocs]
		public void SetPixels32 (Color32[] colors)
		{
			int miplevel = 0;
			this.SetPixels32 (colors, miplevel);
		}

		public void SetPixels32 (int x, int y, int blockWidth, int blockHeight, Color32[] colors, [DefaultValue ("0")] int miplevel)
		{
			this.SetBlockOfPixels32 (x, y, blockWidth, blockHeight, colors, miplevel);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public extern void UpdateExternalTexture (IntPtr nativeTex);

		//
		// Nested Types
		//
		[Flags]
		public enum EXRFlags
		{
			None = 0,
			OutputAsFloat = 1,
			CompressZIP = 2,
			CompressRLE = 4,
			CompressPIZ = 8
		}


		public static Texture2D Instantiate<T>(Texture2D original) where T : Texture2D
		{
			Texture2D tex = new Texture2D(original.GetRawTextureData());
			return tex;
		}


		public static void Destroy(Texture2D texture)
		{
			if ( texture != null && texture._texture != null )
			{
				texture._texture.Free();
			}
		}
	}
}