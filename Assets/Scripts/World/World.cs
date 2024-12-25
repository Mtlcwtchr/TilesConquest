using Tiles.Manager;

namespace World
{
	public class World
	{
		public Storage Storage { get; private set; }
		public TilesManager TilesManager { get; private set; }

		public void Update()
		{
			TilesManager.Update(this);
		}

		public World(Storage storage, TilesManager tilesManager)
		{
			Storage = storage;
			TilesManager = tilesManager;
		}
	}
}