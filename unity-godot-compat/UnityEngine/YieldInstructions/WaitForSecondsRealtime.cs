using Godot;

namespace UnityEngine
{
	public class WaitForSecondsRealtime : CustomYieldInstruction
	{
		public float finishTime;


		public WaitForSecondsRealtime (float seconds)
		{
			finishTime = Time.realtimeSinceStartup + seconds;
		}


		public override bool keepWaiting
		{
			get
			{
				return finishTime > Time.realtimeSinceStartup;
			}
		}
	}
}