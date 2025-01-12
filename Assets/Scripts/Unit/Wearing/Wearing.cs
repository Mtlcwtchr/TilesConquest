using Unit.Config.Wearing;
using Unit.Creation;

namespace Unit.Wearing
{
	public abstract class Wearing<T> : IWearing where T : WearingConfig
	{
		public abstract EWearingSlot Slot { get; }
		public WearingConfig Config { get; }
		protected T WearingConfig { get; }

		protected Wearing(T config)
		{
			Config = WearingConfig = config;
		}

		public abstract void Equip(UnitTemplate unit);
		public abstract void UnEquip(UnitTemplate unit);
	}
}