using System;
using System.Collections.Generic;
using Unit.Config;
using Unit.Config.Wearing;
using UnityEngine;
using UnityEngine.Serialization;

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
			public List<UnitConfig> units;
		}
		
		[FormerlySerializedAs("wearingsByEras")] public List<Era> eras;

		public List<WearingConfig> GetEraWearings(EEra era, int level)
		{
			var allWearings = new List<WearingConfig>();
			for (var i = 0; i < eras.Count; i++)
			{
				if (eras[i].era == era &&
				    eras[i].level == level)
				{
					allWearings.AddRange(eras[i].wearings);
				}
			}

			return allWearings;
		}

		public List<UnitConfig> GetEraUnits(EEra era, int level)
		{
			var allUnits = new List<UnitConfig>();
			for (var i = 0; i < eras.Count; i++)
			{
				if (eras[i].era == era &&
				    eras[i].level == level)
				{
					allUnits.AddRange(eras[i].units);
				}
			}

			return allUnits;
		}
	}
}