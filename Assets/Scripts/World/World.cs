using System.Collections.Generic;
using Tiles.Manager;
using Unit.Raid;
using World.Units;

namespace World
{
	public class World
	{
		public Era.Era Era { get; private set; }
		public Storage Storage { get; private set; }
		public Forge Forge { get; private set; }
		public TilesManager TilesManager { get; private set; }
		
		public RecruitHouse RecruitHouse { get; private set; }
		
		public RaidManager RaidManager { get; private set; }

		public void Update()
		{
			TilesManager.Update(this);
			RaidManager.Update(this);
		}

		public World(Era.Era era, Storage storage, Forge forge, RecruitHouse recruitHouse, TilesManager tilesManager, RaidManager raidManager)
		{
			Era = era;
			Forge = forge;
			Storage = storage;
			RecruitHouse = recruitHouse;
			TilesManager = tilesManager;
			RaidManager = raidManager;
		}
	}
}