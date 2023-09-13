using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	public struct Mathf
	{
		//
		// Static Fields
		//
		public const float PI = 3.14159274f;

		public const float Infinity = float.PositiveInfinity;

		public const float NegativeInfinity = float.NegativeInfinity;

		public const float Deg2Rad = 0.0174532924f;

		public const float Rad2Deg = 57.29578f;

		public static readonly float Epsilon = (!MathfInternal.IsFlushToZeroEnabled) ? MathfInternal.FloatMinDenormal : MathfInternal.FloatMinNormal;

		//
		// Static Methods
		//
		public static int Abs (int value)
		{
			return Math.Abs (value);
		}

		public static float Abs (float f)
		{
			return Math.Abs (f);
		}

		public static float Acos (float f)
		{
			return (float)Math.Acos ((double)f);
		}

		public static bool Approximately (float a, float b)
		{
			return Mathf.Abs (b - a) < Mathf.Max (1E-06f * Mathf.Max (Mathf.Abs (a), Mathf.Abs (b)), Mathf.Epsilon * 8f);
		}

		public static float Asin (float f)
		{
			return (float)Math.Asin ((double)f);
		}

		public static float Atan (float f)
		{
			return (float)Math.Atan ((double)f);
		}

		public static float Atan2 (float y, float x)
		{
			return (float)Math.Atan2 ((double)y, (double)x);
		}

		public static float Ceil (float f)
		{
			return (float)Math.Ceiling ((double)f);
		}

		public static int CeilToInt (float f)
		{
			return (int)Math.Ceiling ((double)f);
		}

		public static float Clamp (float value, float min, float max)
		{
			if (value < min) {
				value = min;
			} else if (value > max) {
				value = max;
			}
			return value;
		}

		public static int Clamp (int value, int min, int max)
		{
			if (value < min) {
				value = min;
			} else if (value > max) {
				value = max;
			}
			return value;
		}

		public static float Clamp01 (float value)
		{
			float result;
			if (value < 0f) {
				result = 0f;
			} else if (value > 1f) {
				result = 1f;
			} else {
				result = value;
			}
			return result;
		}

		[GeneratedByOldBindingsGenerator, ThreadAndSerializationSafe]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern int ClosestPowerOfTwo (int value);

		public static Color CorrelatedColorTemperatureToRGB (float kelvin)
		{
			Color result;
			Mathf.INTERNAL_CALL_CorrelatedColorTemperatureToRGB (kelvin, out result);
			return result;
		}

		public static float Cos (float f)
		{
			return (float)Math.Cos ((double)f);
		}

		public static float DeltaAngle (float current, float target)
		{
			float num = Mathf.Repeat (target - current, 360f);
			if (num > 180f) {
				num -= 360f;
			}
			return num;
		}

		public static float Exp (float power)
		{
			return (float)Math.Exp ((double)power);
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern ushort FloatToHalf (float val);

		public static float Floor (float f)
		{
			return (float)Math.Floor ((double)f);
		}

		public static int FloorToInt (float f)
		{
			return (int)Math.Floor ((double)f);
		}

		public static float Gamma (float value, float absmax, float gamma)
		{
			bool flag = false;
			if (value < 0f) {
				flag = true;
			}
			float num = Mathf.Abs (value);
			float result;
			if (num > absmax) {
				result = ((!flag) ? num : (-num));
			} else {
				float num2 = Mathf.Pow (num / absmax, gamma) * absmax;
				result = ((!flag) ? num2 : (-num2));
			}
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace (float value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern float HalfToFloat (ushort val);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CorrelatedColorTemperatureToRGB (float kelvin, out Color value);

		public static float InverseLerp (float a, float b, float value)
		{
			float result;
			if (a != b) {
				result = Mathf.Clamp01 ((value - a) / (b - a));
			} else {
				result = 0f;
			}
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern bool IsPowerOfTwo (int value);

		public static float Lerp (float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01 (t);
		}

		public static float LerpAngle (float a, float b, float t)
		{
			float num = Mathf.Repeat (b - a, 360f);
			if (num > 180f) {
				num -= 360f;
			}
			return a + num * Mathf.Clamp01 (t);
		}

		public static float LerpUnclamped (float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace (float value);

		internal static bool LineIntersection (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool result2;
			if (num5 == 0f) {
				result2 = false;
			} else {
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				result = new Vector2 (p1.x + num8 * num, p1.y + num8 * num2);
				result2 = true;
			}
			return result2;
		}

		internal static bool LineSegmentIntersection (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool result2;
			if (num5 == 0f) {
				result2 = false;
			} else {
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				if (num8 < 0f || num8 > 1f) {
					result2 = false;
				} else {
					float num9 = (num6 * num2 - num7 * num) / num5;
					if (num9 < 0f || num9 > 1f) {
						result2 = false;
					} else {
						result = new Vector2 (p1.x + num8 * num, p1.y + num8 * num2);
						result2 = true;
					}
				}
			}
			return result2;
		}

		public static float Log (float f, float p)
		{
			return (float)Math.Log ((double)f, (double)p);
		}

		public static float Log (float f)
		{
			return (float)Math.Log ((double)f);
		}

		public static float Log10 (float f)
		{
			return (float)Math.Log10 ((double)f);
		}

		public static float Max (float a, float b)
		{
			return (a <= b) ? b : a;
		}

		public static int Max (int a, int b)
		{
			return (a <= b) ? b : a;
		}

		public static int Max (params int[] values)
		{
			int num = values.Length;
			int result;
			if (num == 0) {
				result = 0;
			} else {
				int num2 = values [0];
				for (int i = 1; i < num; i++) {
					if (values [i] > num2) {
						num2 = values [i];
					}
				}
				result = num2;
			}
			return result;
		}

		public static float Max (params float[] values)
		{
			int num = values.Length;
			float result;
			if (num == 0) {
				result = 0f;
			} else {
				float num2 = values [0];
				for (int i = 1; i < num; i++) {
					if (values [i] > num2) {
						num2 = values [i];
					}
				}
				result = num2;
			}
			return result;
		}

		public static float Min (float a, float b)
		{
			return (a >= b) ? b : a;
		}

		public static int Min (params int[] values)
		{
			int num = values.Length;
			int result;
			if (num == 0) {
				result = 0;
			} else {
				int num2 = values [0];
				for (int i = 1; i < num; i++) {
					if (values [i] < num2) {
						num2 = values [i];
					}
				}
				result = num2;
			}
			return result;
		}

		public static int Min (int a, int b)
		{
			return (a >= b) ? b : a;
		}

		public static float Min (params float[] values)
		{
			int num = values.Length;
			float result;
			if (num == 0) {
				result = 0f;
			} else {
				float num2 = values [0];
				for (int i = 1; i < num; i++) {
					if (values [i] < num2) {
						num2 = values [i];
					}
				}
				result = num2;
			}
			return result;
		}

		public static float MoveTowards (float current, float target, float maxDelta)
		{
			float result;
			if (Mathf.Abs (target - current) <= maxDelta) {
				result = target;
			} else {
				result = current + Mathf.Sign (target - current) * maxDelta;
			}
			return result;
		}

		public static float MoveTowardsAngle (float current, float target, float maxDelta)
		{
			float num = Mathf.DeltaAngle (current, target);
			float result;
			if (-maxDelta < num && num < maxDelta) {
				result = target;
			} else {
				target = current + num;
				result = Mathf.MoveTowards (current, target, maxDelta);
			}
			return result;
		}

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern int NextPowerOfTwo (int value);

		[GeneratedByOldBindingsGenerator]
		[MethodImpl (MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise (float x, float y);

		public static float PingPong (float t, float length)
		{
			t = Mathf.Repeat (t, length * 2f);
			return length - Mathf.Abs (t - length);
		}

		public static float Pow (float f, float p)
		{
			return (float)Math.Pow ((double)f, (double)p);
		}

		internal static long RandomToLong (System.Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes (array);
			return (long)(BitConverter.ToUInt64 (array, 0) & 9223372036854775807uL);
		}

		public static float Repeat (float t, float length)
		{
			return Mathf.Clamp (t - Mathf.Floor (t / length) * length, 0f, length);
		}

		public static float Round (float f)
		{
			return (float)Math.Round ((double)f);
		}

		public static int RoundToInt (float f)
		{
			return (int)Math.Round ((double)f);
		}

		public static float Sign (float f)
		{
			return (f < 0f) ? -1f : 1f;
		}

		public static float Sin (float f)
		{
			return (float)Math.Sin ((double)f);
		}

		[ExcludeFromDocs]
		public static float SmoothDamp (float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		[ExcludeFromDocs]
		public static float SmoothDamp (float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDamp (float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue ("Mathf.Infinity")] float maxSpeed, [DefaultValue ("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max (0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp (num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5) {
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		[ExcludeFromDocs]
		public static float SmoothDampAngle (float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		[ExcludeFromDocs]
		public static float SmoothDampAngle (float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return Mathf.SmoothDampAngle (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothDampAngle (float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue ("Mathf.Infinity")] float maxSpeed, [DefaultValue ("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle (current, target);
			return Mathf.SmoothDamp (current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static float SmoothStep (float from, float to, float t)
		{
			t = Mathf.Clamp01 (t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		public static float Sqrt (float f)
		{
			return (float)Math.Sqrt ((double)f);
		}

		public static float Tan (float f)
		{
			return (float)Math.Tan ((double)f);
		}
	}
}
