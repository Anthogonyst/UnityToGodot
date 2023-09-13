using System;

namespace UnityEngine
{
    public struct Vector3
    {
		Godot.Vector3 vec;

		public const float kEpsilon = 1E-05f;

		public static readonly Vector3 positiveInfinity = new Vector3 (float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		public static readonly Vector3 negativeInfinity = new Vector3 (float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
		public static readonly Vector3 back = new Vector3 (0f, 0f, -1f);
		public static readonly Vector3 forward = new Vector3 (0f, 0f, 1f);
		public static readonly Vector3 right = new Vector3 (1f, 0f, 0f);
		public static readonly Vector3 left = new Vector3 (-1f, 0f, 0f);
		public static readonly Vector3 down = new Vector3 (0f, -1f, 0f);
		public static readonly Vector3 one = new Vector3 (1f, 1f, 1f);
		public static readonly Vector3 zero = new Vector3 (0f, 0f, 0f);
		public static readonly Vector3 up = new Vector3 (0f, 1f, 0f);


		public float x { get { return vec.x; } set { vec.x = value; } }
		public float y { get { return vec.y; } set { vec.y = value; } }
		public float z { get { return vec.z; } set { vec.z = value; } }


		//
		// Constructors
		//
		public Vector3 (float x, float y)
		{
			vec.x = x;
			vec.y = y;
			vec.z = 0f;
		}


		public Vector3(float x, float y, float z)
        {
            vec.x = x;
            vec.y = y;
            vec.z = z;
        }


		//
		// Conversion Methods
		//
		public static implicit operator Godot.Vector3 (Vector3 vec)
		{
			return new Godot.Vector3(vec.x, vec.y, vec.z);
		}


		public static implicit operator Vector3 (Godot.Vector3 vec)
		{
			return new Vector3(vec.x, vec.y, vec.z);
		}


		//
		// Indexer
		//
		public float this [int index] {
			get {
				float result;
				switch (index) {
					case 0:
						result = vec.x;
						break;
					case 1:
						result = vec.y;
						break;
					case 2:
						result = vec.z;
						break;
					default:
						throw new IndexOutOfRangeException ("Invalid Vector3 index!");
				}
				return result;
			}
			set {
				switch (index) {
					case 0:
						vec.x = value;
						break;
					case 1:
						vec.y = value;
						break;
					case 2:
						vec.z = value;
						break;
					default:
						throw new IndexOutOfRangeException ("Invalid Vector3 index!");
				}
			}
		}


		//
		// Operators
		//
		public static Vector3 operator + (Vector3 a, Vector3 b)
		{
			return new Vector3 (a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator / (Vector3 a, float d)
		{
			return new Vector3 (a.x / d, a.y / d, a.z / d);
		}

		public static bool operator == (Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude (lhs - rhs) < 9.99999944E-11f;
		}

		public static bool operator != (Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		public static Vector3 operator * (Vector3 a, float d)
		{
			return new Vector3 (a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator * (float d, Vector3 a)
		{
			return new Vector3 (a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator - (Vector3 a, Vector3 b)
		{
			return new Vector3 (a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator - (Vector3 a)
		{
			return new Vector3 (-a.x, -a.y, -a.z);
		}


		//
		// Properties
		//
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt (vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
			}
		}

		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize (this);
			}
		}

		public float sqrMagnitude
		{
			get
			{
				return vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
			}
		}


		//
		// Static Methods
		//
		public static float Angle (Vector3 from, Vector3 to)
		{
			return Mathf.Acos (Mathf.Clamp (Vector3.Dot (from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}


		public static Vector3 ClampMagnitude (Vector3 vector, float maxLength)
		{
			Vector3 result;
			if (vector.sqrMagnitude > maxLength * maxLength) {
				result = vector.normalized * maxLength;
			} else {
				result = vector;
			}
			return result;
		}

		public static Vector3 Cross (Vector3 lhs, Vector3 rhs)
		{
			return new Vector3 (lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		public static float Distance (Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3 (a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static float Dot (Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		public static Vector3 Lerp (Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01 (t);
			return new Vector3 (a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static Vector3 LerpUnclamped (Vector3 a, Vector3 b, float t)
		{
			return new Vector3 (a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static float Magnitude (Vector3 vector)
		{
			return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static Vector3 Max (Vector3 lhs, Vector3 rhs)
		{
			return new Vector3 (Mathf.Max (lhs.x, rhs.x), Mathf.Max (lhs.y, rhs.y), Mathf.Max (lhs.z, rhs.z));
		}

		public static Vector3 Min (Vector3 lhs, Vector3 rhs)
		{
			return new Vector3 (Mathf.Min (lhs.x, rhs.x), Mathf.Min (lhs.y, rhs.y), Mathf.Min (lhs.z, rhs.z));
		}

		public static Vector3 MoveTowards (Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 a = target - current;
			float magnitude = a.magnitude;
			Vector3 result;
			if (magnitude <= maxDistanceDelta || magnitude < 1.401298E-45f) {
				result = target;
			} else {
				result = current + a / magnitude * maxDistanceDelta;
			}
			return result;
		}

		public static Vector3 Normalize (Vector3 value)
		{
			float num = Vector3.Magnitude (value);
			Vector3 result;
			if (num > 1E-05f) {
				result = value / num;
			} else {
				result = Vector3.zero;
			}
			return result;
		}

		/*public static void OrthoNormalize (ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.Internal_OrthoNormalize2 (ref normal, ref tangent);
		}

		public static void OrthoNormalize (ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.Internal_OrthoNormalize3 (ref normal, ref tangent, ref binormal);
		}*/

		public static Vector3 Project (Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot (onNormal, onNormal);
			Vector3 result;
			if (num < Mathf.Epsilon) {
				result = Vector3.zero;
			} else {
				result = onNormal * Vector3.Dot (vector, onNormal) / num;
			}
			return result;
		}

		public static Vector3 ProjectOnPlane (Vector3 vector, Vector3 planeNormal)
		{
			return vector - Vector3.Project (vector, planeNormal);
		}

		public static Vector3 Reflect (Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Vector3.Dot (inNormal, inDirection) * inNormal + inDirection;
		}

		/*public static Vector3 RotateTowards (Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_RotateTowards (ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out result);
			return result;
		}*/

		public static Vector3 Scale (Vector3 a, Vector3 b)
		{
			return new Vector3 (a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static float SignedAngle (Vector3 from, Vector3 to, Vector3 axis)
		{
			Vector3 normalized = from.normalized;
			Vector3 normalized2 = to.normalized;
			float num = Mathf.Acos (Mathf.Clamp (Vector3.Dot (normalized, normalized2), -1f, 1f)) * 57.29578f;
			float num2 = Mathf.Sign (Vector3.Dot (axis, Vector3.Cross (normalized, normalized2)));
			return num * num2;
		}


		/*public static Vector3 Slerp (Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_Slerp (ref a, ref b, t, out result);
			return result;
		}*/

		/*public static Vector3 SlerpUnclamped (Vector3 a, Vector3 b, float t)
		{
			Vector3 result;
			Vector3.INTERNAL_CALL_SlerpUnclamped (ref a, ref b, t, out result);
			return result;
		}*/

		public static Vector3 SmoothDamp (Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return Vector3.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static Vector3 SmoothDamp (Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static Vector3 SmoothDamp (Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			smoothTime = Mathf.Max (0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector3 vector = current - target;
			Vector3 vector2 = target;
			float maxLength = maxSpeed * smoothTime;
			vector = Vector3.ClampMagnitude (vector, maxLength);
			target = current - vector;
			Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * d;
			Vector3 vector4 = target + (vector + vector3) * d;
			if (Vector3.Dot (vector2 - current, vector4 - vector2) > 0f) {
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		public static float SqrMagnitude (Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		//
		// Methods
		//
		public override bool Equals (object other)
		{
			bool result;
			if (!(other is Vector3)) {
				result = false;
			} else {
				Vector3 vector = (Vector3)other;
				result = (vec.x.Equals (vector.x) && vec.y.Equals (vector.y) && vec.z.Equals (vector.z));
			}
			return result;
		}

		public override int GetHashCode ()
		{
			return vec.x.GetHashCode () ^ vec.y.GetHashCode () << 2 ^ vec.z.GetHashCode () >> 2;
		}

		public void Normalize ()
		{
			float num = Vector3.Magnitude (this);
			if (num > 1E-05f) {
				vec /= num;
			} else {
				vec = Vector3.zero;
			}
		}

		public void Scale (Vector3 scale)
		{
			vec.x *= scale.x;
			vec.y *= scale.y;
			vec.z *= scale.z;
		}

		public void Set (float newX, float newY, float newZ)
		{
			vec.x = newX;
			vec.y = newY;
			vec.z = newZ;
		}

		public string ToString (string format)
		{
			return string.Format ("({0}, {1}, {2})", vec.x.ToString (format), vec.y.ToString (format), vec.z.ToString (format));
		}

		public override string ToString ()
		{
			return string.Format ("({0:F1}, {1:F1}, {2:F1})", vec.x, vec.y, vec.z);
		}
	}
}