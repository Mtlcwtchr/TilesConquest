using System.Collections.Generic;
using UnityEngine;

namespace Unit.Raid.Target
{
	public class ChaseRaidTarget : Target
	{
		private World.World _world;
		
		private Raid _target;
		private Raid _attacker;

		private MoveToTileTarget _moveTarget;
		
		public ChaseRaidTarget(Raid attacker, Raid target, World.World world) : base(attacker)
		{
			_target = target;
			_attacker = attacker;
			_world = world;
		}
		
		public override void Start()
		{
			_target.OnPositionUpdate += PositionUpdate;
			ProceedTargetPosition();
		}

		public override void Advance()
		{
			_moveTarget?.Advance();
		}

		private void ProceedTargetPosition()
		{
			_moveTarget?.Cancel();
			_moveTarget = new MoveToTileTarget(_attacker, _world, _target.Position);
			_moveTarget.Start();
		}

		private void PositionUpdate(List<Vector2Int> arg1, int arg2, int arg3)
		{
			ProceedTargetPosition();
		}
	}
}