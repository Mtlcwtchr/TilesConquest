using Unit.Config.Wearing;
using Unit.Creation;

namespace Unit.Wearing
{
	public class ArmorWearing : Wearing<ArmorConfig>
	{
		public override EWearingSlot Slot => EWearingSlot.Chest;

		public ArmorWearing(ArmorConfig config) : base(config) { }
		
		public override void Equip(UnitTemplate unit)
		{
			unit.Hp += WearingConfig.defence;
		}

		public override void UnEquip(UnitTemplate unit)
		{
			unit.Hp -= WearingConfig.defence;
		}
	}
}