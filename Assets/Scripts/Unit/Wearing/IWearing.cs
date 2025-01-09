using Unit.Config;
using Unit.Creation;

namespace Unit.Wearing
{
	public interface IWearing
	{
		public EWearingSlot Slot { get; }
		
		public WearingConfig Config { get; }
		
		public void Equip(UnitTemplate unit);
		public void UnEquip(UnitTemplate unit);
	}
}