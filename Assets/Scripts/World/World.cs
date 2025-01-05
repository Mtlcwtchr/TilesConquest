using Tiles.Manager;
using Unit.Raid;

namespace World
{
	public class World
	{
		public Storage Storage { get; private set; }
		public TilesManager TilesManager { get; private set; }
		
		public RaidManager RaidManager { get; private set; }

		public void Update()
		{
			TilesManager.Update(this);
			RaidManager.Update(this);
		}

		public World(Storage storage, TilesManager tilesManager, RaidManager raidManager)
		{
			Storage = storage;
			TilesManager = tilesManager;
			RaidManager = raidManager;
		}
	}
}