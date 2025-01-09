using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using UnityEngine;
using Utils;

namespace Unit.Raid
{
	public class RaidView : MonoBehaviour, ISelectable
	{
		[Serializable]
		private class Placeholder
		{
			public int priority;
			public Transform root;
			public Unit unit;
		}

		public event Action<bool> OnSelect;
		
		[SerializeField] private Placeholder[] placeholders;
		
		private Vector2 _currentPosition;
		private Raid _model;

		private List<Placeholder> _placeholders = new ();
		private List<UnitView> _units = new();
		
		public void Init(Raid model)
		{
			_model = model;
			_model.Position = transform.position.V2().I();
			_currentPosition = _model.Position;

			_placeholders = new List<Placeholder>(placeholders);
			
			_model.OnPositionUpdate += UpdatePosition;
			_model.OnUnitAdded += UnitAdded;
			_model.OnUnitRemoved += UnitRemoved;
		}

		private void UnitRemoved(Unit unit)
		{
			_units.Remove(unit.View);
			var placeholder = _placeholders.Find(p => p.unit == unit);
			if (placeholder == null)
				return;

			placeholder.unit = null;
		}

		private void UnitAdded(Unit unit)
		{
			Placeholder bestPlaceholder = null;
			var currDelta = int.MaxValue;
			for (var i = 0; i < _placeholders.Count; i++)
			{
				var placeholder = _placeholders[i];

				if (placeholder.unit != null)
					continue;
				
				var delta = unit.Priority - placeholder.priority;
				if (bestPlaceholder == null || currDelta > delta)
				{
					bestPlaceholder = placeholder;
					currDelta = delta;
				}
			}

			if (bestPlaceholder == null)
				return;

			bestPlaceholder.unit = unit;
			_units.Add(unit.View);
			unit.View.SetPlaceholder(bestPlaceholder.root);
		}

		private void UpdatePosition(List<Vector2Int> path, int indexFrom, int indexTo)
		{
			UpdatePositionByPath(path, indexFrom, indexTo);
		}

		private async void UpdatePositionByPath(List<Vector2Int> path, int indexFrom, int indexTo)
		{
			NotifyUnitsState(EUnitState.Moving);
			var timeFactor = 1f / (indexTo - indexFrom);
			for (var i = indexFrom + 1; i <= indexTo; i++)
			{
				await UpdatePositionOnTime(path[i], timeFactor);
			}
			NotifyUnitsState(EUnitState.Idle);
		}

		private async Task UpdatePositionOnTime(Vector2 desiredPosition, float timeFactor)
		{
			var t = 0f;
			var delay = (int)(10f * timeFactor);
			var tChange = delay / 1000f;
			var startPosition = _currentPosition;
			while (!_currentPosition.Equals(desiredPosition))
			{
				_currentPosition = Vector2.Lerp(startPosition, desiredPosition, t);
				transform.position = _currentPosition.V3();
				t += tChange;
				await Task.Delay(delay);
			}
		}

		private void NotifyUnitsState(EUnitState state)
		{
			for (var i = 0; i < _units.Count; i++)
			{
				_units[i].SetState(state);
			}
		}

		public void Select(bool primary)
		{
			OnSelect?.Invoke(primary);
		}
	}
}