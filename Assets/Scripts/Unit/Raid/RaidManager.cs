using System;
using System.Collections.Generic;
using Tiles.Model;
using Unit.Creation;
using Unit.Raid.Target;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unit.Raid
{
	public class RaidManager
	{
		public event Action<Raid> OnRaidRegistered;
		public event Action<Raid> OnRaidUnRegistered;
		
		private RaidView _viewTemplate;
		
		private List<Raid> _raids;
		private List<RaidView> _views;

		private World.World _world;

		private Raid _selectedRaid;

		public RaidManager(RaidView viewTemplate)
		{
			_raids = new();
			_views = new();

			_viewTemplate = viewTemplate;
		}

		public void SetWorld(World.World world)
		{
			_world = world;
			
			_world.TilesManager.OnTileSelected += TileSelected;
		}

		public void CreateRaid(RaidTemplate template, Vector2Int position)
		{
			var raid = new Raid(template);
			raid.Position = position;

			RegisterRaid(raid);
		}

		public void RegisterRaid(Raid raid)
		{
			_raids.Add(raid);
			
			var raidView = Object.Instantiate(_viewTemplate);
			raidView.Init(raid);
			_views.Add(raidView);
			
			raid.OnSelected += RaidSelected;

			OnRaidRegistered?.Invoke(raid);
		}

		public void UnregisterRaid(Raid raid)
		{
			var raidIndex = _raids.IndexOf(raid);
			_raids.RemoveAt(raidIndex);
			var view = _views[raidIndex];
			Object.Destroy(view.gameObject);
			_views.RemoveAt(raidIndex);

			raid.OnSelected -= RaidSelected;

			OnRaidUnRegistered?.Invoke(raid);
		}
		
		public void Update()
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
				ChaseRaid(raid);
			}
		}

		private void SelectRaid(Raid raid)
		{
			_selectedRaid = raid;
		}

		private void ChaseRaid(Raid raid)
		{
			if (_selectedRaid == null)
				return;
			
			var target = new ChaseRaidTarget(_selectedRaid, raid, _world);
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

		public void RaidEnterBrigade(Raid raid)
		{
			var index = _raids.IndexOf(raid);
			var raidView = _views[index];
			raidView.SetActive(false);
		}

		public void RaidLeaveBrigade(Raid raid)
		{
			var index = _raids.IndexOf(raid);
			var raidView = _views[index];
			raidView.SetActive(true);
		}
	}
}