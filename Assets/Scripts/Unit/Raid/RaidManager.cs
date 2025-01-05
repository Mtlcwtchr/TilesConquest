using System.Collections.Generic;
using Unit.Raid.Target;

namespace Unit.Raid
{
	public class RaidManager
	{
		private List<Raid> _raids;

		private World.World _world;

		public RaidManager()
		{
			_raids = new();
		}

		public void SetWorld(World.World world)
		{
			_world = world;
		}

		public void RegisterRaid(Raid raid)
		{
			_raids.Add(raid);
		}

		public void UnregisterRaid(Raid raid)
		{
			_raids.Remove(raid);
		}
		
		public void Update(World.World world)
		{
			for (var i = 0; i < _raids.Count; i++)
			{
				_raids[i].Update();
			}
		}

		public void MoveToRandom(Raid raid)
		{
			var randomTile = _world.TilesManager.GetRandomActive();

			var target = new MoveToTileTarget(raid, _world, randomTile.Position);
			raid.SetTarget(target);
		}
	}
}