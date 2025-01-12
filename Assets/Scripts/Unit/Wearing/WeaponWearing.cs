using Unit.Config.Wearing;
using Unit.Creation;

namespace Unit.Wearing
{
	public class WeaponWearing : Wearing<WeaponConfig>
	{
		public override EWearingSlot Slot => EWearingSlot.HandL;

		public WeaponWearing(WeaponConfig config) : base(config) { }
		
		public override void Equip(UnitTemplate unit)
		{
			unit.Damage += WearingConfig.damage;
		}

		public override void UnEquip(UnitTemplate unit)
		{
			unit.Damage -= WearingConfig.damage;
		}
	}
}