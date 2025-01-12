using System;
using System.Collections.Generic;
using Unit.Config.Wearing;
using UnityEngine;

namespace World.Era.Config
{
	[CreateAssetMenu(fileName = "EraConfig", menuName = "Era/Config")]
	public class EraConfig : ScriptableObject
	{
		[Serializable]
		public class Era
		{
			public EEra era;
			public int level;
			public List<WearingConfig> wearings;
		}
		
		public List<Era> wearingsByEras;

		public List<WearingConfig> GetEraWearings(EEra era, int level)
		{
			var allWearings = new List<WearingConfig>();
			for (var i = 0; i < wearingsByEras.Count; i++)
			{
				if (wearingsByEras[i].era == era &&
				    wearingsByEras[i].level == level)
				{
					allWearings.AddRange(wearingsByEras[i].wearings);
				}
			}

			return allWearings;
		}
	}
}