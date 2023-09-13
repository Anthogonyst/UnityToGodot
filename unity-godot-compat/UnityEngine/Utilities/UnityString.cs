using System;

namespace UnityEngine
{
	internal sealed class UnityString
	{
		//
		// Static Methods
		//
		public static string Format (string fmt, params object[] args)
		{
			return string.Format (fmt, args);
		}
	}
}
