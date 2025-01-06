using UnityEngine;

namespace Unit
{
	public enum EUnitState
	{
		Idle,
		Moving,
	}

	public static class UnitStateAnimations
	{
		private static readonly int Idle = Animator.StringToHash("Idle");
		private static readonly int Moving = Animator.StringToHash("Moving");
		
		public static int Get(EUnitState state)
		{
			return state switch
			{
				EUnitState.Idle => Idle,
				EUnitState.Moving => Moving,
				_ => Idle
			};
		}
	}
}