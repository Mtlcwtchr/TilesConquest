using System;
using System.Collections.Generic;
using Tiles.Model;
using Unit.Raid.Target;

namespace Unit.Raid
{
	public class RaidManager
	{
		public event Action<Raid> OnRaidSelected; 
		
		private List<Raid> _raids;

		private World.World _world;

		private Raid _selectedRaid;

		public RaidManager()
		{
			_raids = new();
		}

		public void SetWorld(World.World world)
		{
			_world = world;
			
			_world.TilesManager.OnTileSelected += TileSelected;
		}

		public void RegisterRaid(Raid raid)
		{
			_raids.Add(raid);
			
			raid.OnSelected += RaidSelected;
		}

		public void UnregisterRaid(Raid raid)
		{
			_raids.Remove(raid);
			
			raid.OnSelected -= RaidSelected;
		}
		
		public void Update(World.World world)
		{
			for (var i = 0; i < _raids.Count; i++)
			{
				_raids[i].Update();
			}
		}

		private void MoveTo(Raid raid, Tile tile)
		{
			var target = new MoveToTileTarget(raid, _world, tile.Position);
			raid.SetTarget(target);
		}

		private void RaidSelected(Raid raid, bool primary)
		{
			if (primary)
			{
				SelectRaid(raid);
			}
			else
			{
				AttackRaid(raid);
			}
		}

		private void SelectRaid(Raid raid)
		{
			_selectedRaid = raid;
			
			OnRaidSelected?.Invoke(raid);
		}

		private void AttackRaid(Raid raid)
		{
			if (_selectedRaid == null)
				return;
			
			var target = new ChaseRaidTarget(_selectedRaid, raid);
			_selectedRaid.SetTarget(target);
		}

		private void TileSelected(Tile tile, bool primary)
		{
			if (_selectedRaid == null)
				return;

			if (primary)
				return;

			MoveTo(_selectedRaid, tile);
		}
	}
}