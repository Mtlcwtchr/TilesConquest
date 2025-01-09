using System;
using System.Collections.Generic;
using Game;
using Unit.Raid.Target;
using UnityEngine;

namespace Unit.Raid
{
	public class Raid
	{
		public event Action<Raid, bool> OnSelected;
		public event Action<List<Vector2Int>, int, int> OnPositionUpdate;
		public event Action<Unit> OnUnitAdded;
		public event Action<Unit> OnUnitRemoved;
		
		private const int MaxUnits = 5;

		private Player _owner;

		private ITarget _target;

		private RaidView _view;

		public Vector2Int Position { get; set; }

		public int Speed { get; private set; }
		public int MaxHp { get; private set; }
		public int Hp { get; private set; }
		public int Damage { get; private set; }

		public float RelativeHp => Hp / (float)MaxHp;
			
		public List<Unit> Units { get; }

		public Raid(RaidView view, Player owner)
		{
			_view = view;
			_owner = owner;
			Units = new List<Unit>(MaxUnits);
			
			_view.OnSelect += ViewSelect;
		}

		public void Update()
		{
			_target?.Advance();
		}

		public bool TryAddUnit(Unit unit)
		{
			if (Units.Count >= MaxUnits)
				return false;

			Units.Add(unit);
			CalculateUnitsData();
			OnUnitAdded?.Invoke(unit);
			return true;
		}

		public bool TryRemoveUnit(Unit unit)
		{
			if (Units.Remove(unit))
			{
				CalculateUnitsData();
				OnUnitRemoved?.Invoke(unit);
				return true;
			}

			return false;
		}

		private void CalculateUnitsData()
		{
			MaxHp = 0;
			Hp = 0;
			Damage = 0;
			Speed = int.MaxValue;
			
			for (var i = 0; i < Units.Count; i++)
			{
				var unit = Units[i];

				MaxHp += unit.MaxHp;
				Hp += unit.Hp;
				Damage += unit.Damage;

				Speed = Mathf.Min(unit.Speed, Speed);
			}
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

		private void ViewSelect(bool primary)
		{
			OnSelected?.Invoke(this, primary);
		}
	}
}