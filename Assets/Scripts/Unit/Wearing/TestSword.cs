using Unit.Config;
using Unit.Creation;

namespace Unit.Wearing
{
	public class TestSword : IWearing
	{
		public EWearingSlot Slot => EWearingSlot.HandL;
		public WearingConfig Config { get; }

		public TestSword(WearingConfig config)
		{
			Config = config;
		}
		
		public void Equip(UnitTemplate unit)
		{
			unit.Damage += 10;
		}

		public void UnEquip(UnitTemplate unit)
		{
			unit.Damage -= 10;
		}
	}
}