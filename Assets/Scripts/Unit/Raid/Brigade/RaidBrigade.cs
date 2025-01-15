using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Unit.Raid.Brigade
{
	public class RaidBrigade
	{
		public event Action<RaidBrigade> OnDisassemble; 
		
		public event Action<Raid> OnRaidLeaveBrigade; 
		
		public event Action<RaidBrigade, bool> OnSelected;

		private Raid _root;
		private Raid Root
		{
			get => _root;
			set
			{
				_root = value;
				
				Position = Root.Position;
				Root.OnPositionUpdate += PositionUpdate;
			}
		}
		
		public List<Raid> Raids { get; }
		
		public List<Player> Players { get; }

		public Dictionary<Player, List<Raid>> RaidsByPlayer { get; }
		
		public Vector2Int Position { get; set; }

		public bool IsValid => Raids.Count > 1;

		public RaidBrigade(List<Raid> raids)
		{
			Raids = new();
			Players = new();
			RaidsByPlayer = new();
			
			raids.ForEach(AddRaid);
		}

		private void PositionUpdate(List<Vector2Int> path, int from, int to)
		{
			Position = Root.Position;
		}

		public void AddRaid(Raid raid)
		{
			Root ??= raid;
			
			Raids.Add(raid);
			var player = raid.Owner;

			if(!Players.Contains(player))
			{
				Players.Add(player);
			}

			var playerRaids = RaidsByPlayer.GetValueOrDefault(player);
			if (playerRaids == null)
			{
				playerRaids = new();
				RaidsByPlayer.Add(player, playerRaids);
			}

			playerRaids.Add(raid);
			
			raid.OnTargetSet += TargetSet;
		}

		public void AddRaids(List<Raid> raids)
		{
			for (var i = 0; i < raids.Count; i++)
			{
				AddRaid(raids[i]);
			}
		}

		public void RemoveRaid(Raid raid)
		{
			raid.OnTargetSet -= TargetSet;
			
			Raids.Remove(raid);
			RaidsByPlayer[raid.Owner].Remove(raid);
			OnRaidLeaveBrigade?.Invoke(raid);

			if (Raids.Count < 1)
				return;
			
			if (Raids.Count < 2)
			{
				Disassemble();
			}
			
			if (raid == Root)
			{
				Root = Raids[0];
			}
		}

		public void Disassemble()
		{
			for (var i = 0; i < Raids.Count; i++)
			{
				RemoveRaid(Raids[i--]);
			}

			OnDisassemble?.Invoke(this);
		}

		public List<Raid> Get(Player player)
		{
			return RaidsByPlayer.GetValueOrDefault(player);
		}

		public void Select(bool primary)
		{
			OnSelected?.Invoke(this, primary);
		}

		private void TargetSet(Raid raid)
		{
			RemoveRaid(raid);
		}
	}
}