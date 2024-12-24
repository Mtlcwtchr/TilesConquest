using System.Collections.Generic;
using UnityEngine;

namespace Tiles.Config
{
	[CreateAssetMenu(fileName = "PoolConfig", menuName = "Tiles/Pool/Config")]
	public class TilesPoolConfig : ScriptableObject
	{
		public List<TileFillConfig> availableFills;
	}
}