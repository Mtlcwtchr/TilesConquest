using Unit.Config.Wearing;
using Unit.Creation;

namespace Unit.Wearing
{
	public class HelmetWearing : Wearing<HelmetConfig>
	{
		public override EWearingSlot Slot => EWearingSlot.Head;

		public HelmetWearing(HelmetConfig config) : base(config) { }
		
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