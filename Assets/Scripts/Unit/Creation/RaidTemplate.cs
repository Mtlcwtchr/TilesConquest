using System;
using System.Collections.Generic;
using Game;

namespace Unit.Creation
{
	public class RaidTemplate
	{
		public event Action<UnitTemplate> OnUnitAdded;
		public event Action<UnitTemplate> OnUnitRemoved;
		
		private const int MaxUnits = 5;
		
		public List<UnitTemplate> Units { get; }
		
		public Player Owner { get; private set; }

		public RaidTemplate(Player owner)
		{
			Owner = owner;
			Units = new List<UnitTemplate>(MaxUnits);
		}

		public bool TryAddUnit(UnitTemplate unit)
		{
			if (Units.Count >= MaxUnits)
				return false;

			Units.Add(unit);
			OnUnitAdded?.Invoke(unit);
			return true;
		}

		public bool TryRemoveUnit(UnitTemplate unit)
		{
			if (Units.Remove(unit))
			{
				OnUnitRemoved?.Invoke(unit);
				return true;
			}

			return false;
		}
	}
}