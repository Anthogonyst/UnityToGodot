using System;
using System.Runtime.InteropServices;

namespace UnityEngineInternal
{
	[StructLayout (LayoutKind.Sequential, Size = 1)]
	public struct MathfInternal
	{
		//
		// Static Fields
		//
		public static volatile float FloatMinNormal = 1.17549435E-38f;

		public static volatile float FloatMinDenormal = 1.401298E-45f;

		public static bool IsFlushToZeroEnabled = MathfInternal.FloatMinDenormal == 0f;
	}
}