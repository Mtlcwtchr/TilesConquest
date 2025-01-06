using System;
using System.Collections.Generic;
using Game;
using Unit.Raid.Target;
using UnityEngine;

namespace Unit.Raid
{
	public class Raid
	{
		public event Action<List<Vector2Int>, int, int> OnPositionUpdate;
		public event Action<Unit> OnUnitAdded;
		public event Action<Unit> OnUnitRemoved;
		
		private const int MaxUnits = 5;

		private Player _owner;
		private List<Unit> _units;

		private ITarget _target;

		public Vector2Int Position { get; set; }

		public int Speed { get; private set; } = 2;

		public Raid(Player owner)
		{
			_owner = owner;
			_units = new List<Unit>(MaxUnits);
		}

		public void Update()
		{
			_target?.Advance();
		}

		public bool TryAddUnit(Unit unit)
		{
			if (_units.Count >= MaxUnits)
				return false;

			_units.Add(unit);
			OnUnitAdded?.Invoke(unit);
			return true;
		}

		public bool TryRemoveUnit(Unit unit)
		{
			if (_units.Remove(unit))
			{
				OnUnitRemoved?.Invoke(unit);
				return true;
			}

			return false;
		}

		public void SetTarget(ITarget target)
		{
			_target?.Cancel();

			_target = target;
			_target.Start();
		}

		public void Move(List<Vector2Int> path, int indexFrom, int indexTo)
		{
			Position = path[indexTo];
			OnPositionUpdate?.Invoke(path, indexFrom, indexTo);
		}
	}
}