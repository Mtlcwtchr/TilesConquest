using System.Collections.Generic;
using Unit.Config;
using World.Era;

namespace World.Units
{
	public class RecruitHouse
	{
		private Era.Era _era;
		
		public List<UnitConfig> Archetypes { get; set; }

		public RecruitHouse(Era.Era era)
		{
			_era = era;
			Archetypes = new();
			
			foreach (var (eraType, level) in _era.EraLevel)
			{
				Archetypes.AddRange(_era.Config.GetEraUnits(eraType, level));
			}
			
			_era.OnEraAdvanced += EraAdvanced;
		}

		public void AddArchetype(UnitConfig config)
		{
			Archetypes.Add(config);
		}

		public void RemoveArchetype(UnitConfig config)
		{
			Archetypes.Remove(config);
		}

		private void AddArchetypes(List<UnitConfig> archetypes)
		{
			Archetypes.AddRange(archetypes);
		}

		private void EraAdvanced(EEra era, int level)
		{
			AddArchetypes(_era.Config.GetEraUnits(era, level));
		}
	}
}