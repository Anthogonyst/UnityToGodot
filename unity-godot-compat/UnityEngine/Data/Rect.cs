using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Rect
	{
		//
		// Fields
		//
		private float m_XMin;

		private float m_YMin;

		private float m_Width;

		private float m_Height;

		//
		// Static Properties
		//
		public static Rect zero {
			get {
				return new Rect (0f, 0f, 0f, 0f);
			}
		}

		//
		// Properties
		//
		[Obsolete ("use yMax")]
		public float bottom {
			get {
				return this.m_YMin + this.m_Height;
			}
		}

		public Vector2 center {
			get {
				return new Vector2 (this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set {
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		public float height {
			get {
				return this.m_Height;
			}
			set {
				this.m_Height = value;
			}
		}

		[Obsolete ("use xMin")]
		public float left {
			get {
				return this.m_XMin;
			}
		}

		public Vector2 max {
			get {
				return new Vector2 (this.xMax, this.yMax);
			}
			set {
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		public Vector2 min {
			get {
				return new Vector2 (this.xMin, this.yMin);
			}
			set {
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		public Vector2 position {
			get {
				return new Vector2 (this.m_XMin, this.m_YMin);
			}
			set {
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		[Obsolete ("use xMax")]
		public float right {
			get {
				return this.m_XMin + this.m_Width;
			}
		}

		public Vector2 size {
			get {
				return new Vector2 (this.m_Width, this.m_Height);
			}
			set {
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		[Obsolete ("use yMin")]
		public float top {
			get {
				return this.m_YMin;
			}
		}

		public float width {
			get {
				return this.m_Width;
			}
			set {
				this.m_Width = value;
			}
		}

		public float x {
			get {
				return this.m_XMin;
			}
			set {
				this.m_XMin = value;
			}
		}

		public float xMax {
			get {
				return this.m_Width + this.m_XMin;
			}
			set {
				this.m_Width = value - this.m_XMin;
			}
		}

		public float xMin {
			get {
				return this.m_XMin;
			}
			set {
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		public float y {
			get {
				return this.m_YMin;
			}
			set {
				this.m_YMin = value;
			}
		}

		public float yMax {
			get {
				return this.m_Height + this.m_YMin;
			}
			set {
				this.m_Height = value - this.m_YMin;
			}
		}

		public float yMin {
			get {
				return this.m_YMin;
			}
			set {
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		//
		// Constructors
		//
		public Rect (float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		public Rect (Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		public Rect (Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		//
		// Static Methods
		//
		public static Rect MinMaxRect (float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect (xmin, ymin, xmax - xmin, ymax - ymin);
		}

		public static Vector2 NormalizedToPoint (Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2 (Mathf.Lerp (rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp (rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		private static Rect OrderMinMax (Rect rect)
		{
			if (rect.xMin > rect.xMax) {
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			if (rect.yMin > rect.yMax) {
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		public static Vector2 PointToNormalized (Rect rectangle, Vector2 point)
		{
			return new Vector2 (Mathf.InverseLerp (rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp (rectangle.y, rectangle.yMax, point.y));
		}

		//
		// Methods
		//
		public bool Contains (Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		public bool Contains (Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		public bool Contains (Vector3 point, bool allowInverse)
		{
			bool result;
			if (!allowInverse) {
				result = this.Contains (point);
			} else {
				bool flag = false;
				if ((this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax)) {
					flag = true;
				}
				result = (flag && ((this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax)));
			}
			return result;
		}

		public override bool Equals (object other)
		{
			bool result;
			if (!(other is Rect)) {
				result = false;
			} else {
				Rect rect = (Rect)other;
				result = (this.x.Equals (rect.x) && this.y.Equals (rect.y) && this.width.Equals (rect.width) && this.height.Equals (rect.height));
			}
			return result;
		}

		public override int GetHashCode ()
		{
			return this.x.GetHashCode () ^ this.width.GetHashCode () << 2 ^ this.y.GetHashCode () >> 2 ^ this.height.GetHashCode () >> 1;
		}

		public bool Overlaps (Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		public bool Overlaps (Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse) {
				rect = Rect.OrderMinMax (rect);
				other = Rect.OrderMinMax (other);
			}
			return rect.Overlaps (other);
		}

		public void Set (float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		public override string ToString ()
		{
			return UnityString.Format ("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", new object[] {
				this.x,
				this.y,
				this.width,
				this.height
			});
		}

		public string ToString (string format)
		{
			return UnityString.Format ("(x:{0}, y:{1}, width:{2}, height:{3})", new object[] {
				this.x.ToString (format),
				this.y.ToString (format),
				this.width.ToString (format),
				this.height.ToString (format)
			});
		}

		//
		// Operators
		//
		public static bool operator == (Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		public static bool operator != (Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}
	}
}
