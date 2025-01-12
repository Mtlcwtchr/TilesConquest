using System.Collections.Generic;
using System.Linq;
using Unit.Config.Wearing;
using Unit.Wearing;
using World.Era;

namespace World
{
	public class Forge
	{
		private Dictionary<EWearingSlot, List<IWearing>> _availableWearings;

		private Era.Era _era;
		
		public Forge(Era.Era era)
		{
			_era = era;
			_availableWearings = new();

			var allWearings = new List<WearingConfig>();
			
			foreach (var (eraType, level) in _era.EraLevel)
			{
				allWearings.AddRange(_era.Config.GetEraWearings(eraType, level));
			}

			_era.OnEraAdvanced += EraAdvanced;
			
			AddWearings(allWearings);
		}

		public List<IWearing> Get(EWearingSlot slot)
		{
			return _availableWearings.GetValueOrDefault(slot);
		}
		
		private void AddWearings(List<WearingConfig> wearings)
		{
			for (var i = 0; i < wearings.Count; i++)
			{
				var wearing = wearings[i];
				List<IWearing> slotList = _availableWearings.GetValueOrDefault(wearing.slot);
				
				if (slotList == null)
				{
					slotList = new List<IWearing>();
					_availableWearings.Add(wearing.slot, slotList);
				}
				
				if(slotList.Any(w => w.Config == wearing))
					continue;
				
				slotList.Add(wearing.Create());
			}
		}

		private void EraAdvanced(EEra era, int level)
		{
			AddWearings(_era.Config.GetEraWearings(era, level));
		}
	}
}