using System;
using System.Collections.Generic;
using Unit.Creation;
using Unit.Visual;
using UnityEngine;

namespace UI.Raid.Creation
{
	public class RaidTemplateView : MonoBehaviour
	{
		[Serializable]
		private class Placeholder
		{
			public int priority;
			public Transform root;
			public UnitTemplate unit;
		}
		
		[SerializeField] private Placeholder[] placeholders;

		private List<UnitVisualisation> _units = new();
		private List<Placeholder> _placeholders;

		public void Init(RaidTemplate model)
		{
			_placeholders = new List<Placeholder>(placeholders);
			
			for (var i = 0; i < model.Units.Count; i++)
			{
				UnitAdded(model.Units[i]);
			}
			
			model.OnUnitAdded += UnitAdded;
			model.OnUnitRemoved += UnitRemoved;
		}

		private void UnitRemoved(UnitTemplate unit)
		{
			_units.Remove(unit.View);
			var placeholder = _placeholders.Find(p => p.unit == unit);
			if (placeholder == null)
				return;

			placeholder.unit = null;
		}

		private void UnitAdded(UnitTemplate unit)
		{
			Placeholder bestPlaceholder = null;
			var currDelta = int.MaxValue;
			for (var i = 0; i < _placeholders.Count; i++)
			{
				var placeholder = _placeholders[i];

				if (placeholder.unit != null)
					continue;
				
				var delta = unit.Priority - placeholder.priority;
				if (bestPlaceholder == null || Math.Abs(currDelta) > Math.Abs(delta))
				{
					bestPlaceholder = placeholder;
					currDelta = delta;
				}
			}

			if (bestPlaceholder == null)
				return;

			bestPlaceholder.unit = unit;
			unit.View = unit.CreateVisualisation(bestPlaceholder.root, LayerMask.NameToLayer("UI"));
			unit.View.transform.localScale = new (100, 100, 1);
			unit.View.transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}
}