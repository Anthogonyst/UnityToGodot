using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Quaternion
	{
		//
		// Static Fields
		//
		private static readonly Quaternion identityQuaternion = new Quaternion (0f, 0f, 0f, 1f);

		public const float kEpsilon = 1E-06f;

		//
		// Fields
		//
		public float x;

		public float y;

		public float z;

		public float w;

		//
		// Static Properties
		//
		public static Quaternion identity {
			get {
				return Quaternion.identityQuaternion;
			}
		}

		//
		// Properties
		//
		public Vector3 eulerAngles {
			get {
				return Quaternion.Internal_MakePositive (Quaternion.Internal_ToEulerRad (this) * 57.29578f);
			}
			set {
				this = Quaternion.Internal_FromEulerRad (value * 0.0174532924f);
			}
		}

		//
		// Indexer
		//
		public float this [int index] {
			get {
				float result;
				switch (index) {
					case 0:
						result = this.x;
						break;
					case 1:
						result = this.y;
						break;
					case 2:
						result = this.z;
						break;
					case 3:
						result = this.w;
						break;
					default:
						throw new IndexOutOfRangeException ("Invalid Quaternion index!");
				}
				return result;
			}
			set {
				switch (index) {
					case 0:
						this.x = value;
						break;
					case 1:
						this.y = value;
						break;
					case 2:
						this.z = value;
						break;
					case 3:
						this.w = value;
						break;
					default:
						throw new IndexOutOfRangeException ("Invalid Quaternion index!");
				}
			}
		}

		//
		// Constructors
		//
		public Quaternion (float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		//
		// Static Methods
		//
		public static float Angle (Quaternion a, Quaternion b)
		{
			float f = Quaternion.Dot (a, b);
			return Mathf.Acos (Mathf.Min (Mathf.Abs (f), 1f)) * 2f * 57.29578f;
		}

		[ThreadAndSerializationSafe]
		public static Quaternion AngleAxis (float angle, Vector3 axis)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_AngleAxis (angle, ref axis, out result);
			return result;
		}

		[Obsolete ("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle (Vector3 axis, float angle)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_AxisAngle (ref axis, angle, out result);
			return result;
		}

		public static float Dot (Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		public static Quaternion Euler (Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad (euler * 0.0174532924f);
		}

		public static Quaternion Euler (float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad (new Vector3 (x, y, z) * 0.0174532924f);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles (Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad (euler);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles (float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad (new Vector3 (x, y, z));
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation (Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad (euler);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation (float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad (new Vector3 (x, y, z));
		}

		public static Quaternion FromToRotation (Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_FromToRotation (ref fromDirection, ref toDirection, out result);
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AngleAxis (float angle, ref Vector3 axis, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AxisAngle (ref Vector3 axis, float angle, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_FromToRotation (ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_FromEulerRad (ref Vector3 euler, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ToAxisAngleRad (ref Quaternion q, out Vector3 axis, out float angle);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ToEulerRad (ref Quaternion rotation, out Vector3 value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Inverse (ref Quaternion rotation, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Lerp (ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LerpUnclamped (ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LookRotation (ref Vector3 forward, ref Vector3 upwards, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Slerp (ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SlerpUnclamped (ref Quaternion a, ref Quaternion b, float t, out Quaternion value);

		private static Quaternion Internal_FromEulerRad (Vector3 euler)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Internal_FromEulerRad (ref euler, out result);
			return result;
		}

		private static Vector3 Internal_MakePositive (Vector3 euler)
		{
			float num = -0.005729578f;
			float num2 = 360f + num;
			if (euler.x < num) {
				euler.x += 360f;
			} else if (euler.x > num2) {
				euler.x -= 360f;
			}
			if (euler.y < num) {
				euler.y += 360f;
			} else if (euler.y > num2) {
				euler.y -= 360f;
			}
			if (euler.z < num) {
				euler.z += 360f;
			} else if (euler.z > num2) {
				euler.z -= 360f;
			}
			return euler;
		}

		private static void Internal_ToAxisAngleRad (Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.INTERNAL_CALL_Internal_ToAxisAngleRad (ref q, out axis, out angle);
		}

		private static Vector3 Internal_ToEulerRad (Quaternion rotation)
		{
			Vector3 result;
			Quaternion.INTERNAL_CALL_Internal_ToEulerRad (ref rotation, out result);
			return result;
		}

		public static Quaternion Inverse (Quaternion rotation)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Inverse (ref rotation, out result);
			return result;
		}

		public static Quaternion Lerp (Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Lerp (ref a, ref b, t, out result);
			return result;
		}

		public static Quaternion LerpUnclamped (Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_LerpUnclamped (ref a, ref b, t, out result);
			return result;
		}

		[ExcludeFromDocs]
		public static Quaternion LookRotation (Vector3 forward)
		{
			Vector3 up = Vector3.up;
			Quaternion result;
			Quaternion.INTERNAL_CALL_LookRotation (ref forward, ref up, out result);
			return result;
		}

		public static Quaternion LookRotation (Vector3 forward, [DefaultValue ("Vector3.up")] Vector3 upwards)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_LookRotation (ref forward, ref upwards, out result);
			return result;
		}

		public static Quaternion RotateTowards (Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle (from, to);
			Quaternion result;
			if (num == 0f) {
				result = to;
			} else {
				float t = Mathf.Min (1f, maxDegreesDelta / num);
				result = Quaternion.SlerpUnclamped (from, to, t);
			}
			return result;
		}

		public static Quaternion Slerp (Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_Slerp (ref a, ref b, t, out result);
			return result;
		}

		public static Quaternion SlerpUnclamped (Quaternion a, Quaternion b, float t)
		{
			Quaternion result;
			Quaternion.INTERNAL_CALL_SlerpUnclamped (ref a, ref b, t, out result);
			return result;
		}

		[Obsolete ("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public static Vector3 ToEulerAngles (Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad (rotation);
		}

		//
		// Methods
		//
		public override bool Equals (object other)
		{
			bool result;
			if (!(other is Quaternion)) {
				result = false;
			} else {
				Quaternion quaternion = (Quaternion)other;
				result = (this.x.Equals (quaternion.x) && this.y.Equals (quaternion.y) && this.z.Equals (quaternion.z) && this.w.Equals (quaternion.w));
			}
			return result;
		}

		public override int GetHashCode ()
		{
			return this.x.GetHashCode () ^ this.y.GetHashCode () << 2 ^ this.z.GetHashCode () >> 2 ^ this.w.GetHashCode () >> 1;
		}

		public void Set (float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		[Obsolete ("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetAxisAngle (Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle (axis, angle);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles (float x, float y, float z)
		{
			this.SetEulerRotation (new Vector3 (x, y, z));
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles (Vector3 euler)
		{
			this = Quaternion.EulerRotation (euler);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation (Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad (euler);
		}

		[Obsolete ("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation (float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad (new Vector3 (x, y, z));
		}

		public void SetFromToRotation (Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation (fromDirection, toDirection);
		}

		[ExcludeFromDocs]
		public void SetLookRotation (Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation (view, up);
		}

		public void SetLookRotation (Vector3 view, [DefaultValue ("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation (view, up);
		}

		public void ToAngleAxis (out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad (this, out axis, out angle);
			angle *= 57.29578f;
		}

		[Obsolete ("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void ToAxisAngle (out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad (this, out axis, out angle);
		}

		[Obsolete ("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEuler ()
		{
			return Quaternion.Internal_ToEulerRad (this);
		}

		[Obsolete ("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEulerAngles ()
		{
			return Quaternion.Internal_ToEulerRad (this);
		}

		public override string ToString ()
		{
			return UnityString.Format ("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] {
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		public string ToString (string format)
		{
			return UnityString.Format ("({0}, {1}, {2}, {3})", new object[] {
				this.x.ToString (format),
				this.y.ToString (format),
				this.z.ToString (format),
				this.w.ToString (format)
			});
		}

		//
		// Operators
		//
		public static bool operator == (Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot (lhs, rhs) > 0.999999f;
		}

		public static bool operator != (Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		public static Quaternion operator * (Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion (lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static Vector3 operator * (Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 result = new Vector3();
			result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return result;
		}
	}
}
