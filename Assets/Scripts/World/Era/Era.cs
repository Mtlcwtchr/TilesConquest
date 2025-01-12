using System;
using System.Collections.Generic;
using World.Era.Config;

namespace World.Era
{
	public class Era
	{
		public event Action<EEra, int> OnEraAdvanced; 
		
		public EraConfig Config { get; }

		public Dictionary<EEra, int> EraLevel { get; }

		public Era(EraConfig config)
		{
			Config = config;
			EraLevel = new()
			{
				{ EEra.Tech, 1 },
				{ EEra.Magic, 1 },
				{ EEra.Nature, 1 }
			};
		}

		public void AdvanceEra(EEra era, int level)
		{
			EraLevel[era] = level;
			OnEraAdvanced?.Invoke(era, level);
		}
	}
}