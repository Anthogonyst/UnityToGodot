using System;

namespace UnityEngine
{
	public class GUIStyle
	{
		public Font font;
		public FontStyle fontStyle;
		public bool wordWrap;
		public TextAnchor alignment;

		public GUIStyle() => throw new NotImplementedException();
		public GUIStyle(GUIStyle original) => throw new NotImplementedException();


		public float CalcHeight(GUIContent content, float width)
		{
			throw new NotImplementedException();
		}
	}
}