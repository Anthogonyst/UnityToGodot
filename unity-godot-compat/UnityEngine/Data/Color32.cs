using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[IL2CPPStructAlignment (Align = 4), UsedByNativeCode]
	public struct Color32
	{
		//
		// Fields
		//
		public byte r;

		public byte g;

		public byte b;

		public byte a;

		//
		// Constructors
		//
		public Color32 (byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		//
		// Static Methods
		//
		public static Color32 Lerp (Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01 (t);
			return new Color32 ((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		public static Color32 LerpUnclamped (Color32 a, Color32 b, float t)
		{
			return new Color32 ((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		//
		// Methods
		//
		public override string ToString ()
		{
			return UnityString.Format ("RGBA({0}, {1}, {2}, {3})", new object[] {
				this.r,
				this.g,
				this.b,
				this.a
			});
		}

		public string ToString (string format)
		{
			return UnityString.Format ("RGBA({0}, {1}, {2}, {3})", new object[] {
				this.r.ToString (format),
				this.g.ToString (format),
				this.b.ToString (format),
				this.a.ToString (format)
			});
		}

		//
		// Operators
		//
		public static implicit operator Color32 (Color c)
		{
			return new Color32 ((byte)(Mathf.Clamp01 (c.r) * 255f), (byte)(Mathf.Clamp01 (c.g) * 255f), (byte)(Mathf.Clamp01 (c.b) * 255f), (byte)(Mathf.Clamp01 (c.a) * 255f));
		}

		public static implicit operator Color (Color32 c)
		{
			return new Color ((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}
	}
}
