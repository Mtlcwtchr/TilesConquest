using System.Collections.Generic;
using Unit.Config;
using Unit.Creation;
using Unit.Wearing;

namespace Unit
{
	public class Unit
	{
		public int MaxHp { get; }
		public int Hp { get; }
		public int Damage { get; }
		public int Speed { get; }
		public int Priority { get; }
		
		public float RelativeHp => Hp / (float)MaxHp;

		public UnitView View { get; set; }

		public Dictionary<EWearingSlot, IWearing> Wearings { get; }

		public UnitConfig Config { get; }
		
		public UnitTemplate Template { get; }

		public Unit(UnitTemplate unit)
		{
			Template = unit;
			Config = unit.Config;
			
			MaxHp = unit.Hp;
			Hp = unit.Hp;
			Damage = unit.Damage;
			Speed = unit.Speed;
			Priority = unit.Priority;

			Wearings = new(unit.Wearings);
		}
	}
}