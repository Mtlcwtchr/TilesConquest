using System;
using System.Collections.Generic;
using Game;
using Unit.Raid.Target;
using UnityEngine;

namespace Unit.Raid
{
	public class Raid
	{
		public event Action<Vector2Int> OnPositionUpdate;
		
		private const int MaxUnits = 5;

		private Player _owner;
		private List<Unit> _units;
		private Vector2Int _position;

		private ITarget _target;

		public Vector2Int Position
		{
			get => _position;
			set
			{
				if (value == _position)
					return;

				_position = value;
				OnPositionUpdate?.Invoke(_position);
			}
		}

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
			return true;
		}

		public bool TryRemoveUnit(Unit unit)
		{
			return _units.Remove(unit);
		}

		public void SetTarget(ITarget target)
		{
			_target?.Cancel();

			_target = target;
			_target.Start();
		}

		public void Move(Vector2Int position)
		{
			Position = position;
		}
	}
}