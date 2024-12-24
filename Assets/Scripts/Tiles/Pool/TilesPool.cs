using System.Collections.Generic;
using Tiles.Config;
using Utils;

namespace Tiles.Pool
{
	public class TilesPool
	{
		private List<TileFillConfig> _tilesInPool;

		public TilesPool(TilesPoolConfig config)
		{
			_tilesInPool = config.availableFills;
		}

		public TileFillConfig Get()
		{
			return _tilesInPool.GetRandom();
		}
	}
}