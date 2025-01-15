using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Unit.Raid.Brigade
{
	public class RaidBrigadeManager
	{
		public event Action<RaidBrigade> OnBrigadeSelected; 
		
		private RaidBrigadeView _viewTemplate;

		private List<RaidBrigade> _brigades = new();
		private List<RaidBrigadeView> _brigadeViews = new();

		private List<Raid> _singleRaids = new();

		private RaidManager _raidManager;

		public RaidBrigadeManager(RaidManager raidManager, RaidBrigadeView viewTemplate)
		{
			_viewTemplate = viewTemplate;
			_raidManager = raidManager;
			
			_raidManager.OnRaidRegistered += RaidRegistered;
		}

		public void Update()
		{
			CheckBrigades();
			CheckNewBrigades();
		}

		private void RegisterBrigade(RaidBrigade brigade)
		{
			_brigades.Add(brigade);

			var brigadeView = Object.Instantiate(_viewTemplate);
			brigadeView.Init(brigade);
			_brigadeViews.Add(brigadeView);

			brigade.OnRaidLeaveBrigade += LeaveBrigade;
			brigade.OnSelected += BrigadeSelected;
			brigade.OnDisassemble += BrigadeDisassemble;
		}

		private void UnregisterBrigade(RaidBrigade brigade)
		{
			var brigadeIndex = _brigades.IndexOf(brigade);
			_brigades.RemoveAt(brigadeIndex);
			var view = _brigadeViews[brigadeIndex];
			Object.Destroy(view.gameObject);
			_brigadeViews.RemoveAt(brigadeIndex);
			
			brigade.OnRaidLeaveBrigade -= LeaveBrigade;
			brigade.OnSelected -= BrigadeSelected;
			brigade.OnDisassemble -= BrigadeDisassemble;
		}

		private void CheckBrigades()
		{
			for (var i = 0; i < _brigades.Count; i++)
			{
				var brigade = _brigades[i];
				CheckBrigadeNeighbors(brigade);

				if (!brigade.IsValid)
				{
					brigade.Disassemble();
					UnregisterBrigade(brigade);
					i--;
				}
			}
		}

		private void CheckBrigadeNeighbors(RaidBrigade brigade)
		{
			var allNeighbors = _singleRaids.FindAll(raid => raid.Position == brigade.Position);
			brigade.AddRaids(allNeighbors);
			for (var i = 0; i < allNeighbors.Count; i++)
			{
				EnterBrigade(allNeighbors[i]);
			}
		}

		private void CheckNewBrigades()
		{
			for (var i = 0; i < _singleRaids.Count; i++)
			{
				if (_singleRaids.Count < 1)
					return;
				
				var raid = _singleRaids[i];
				var neighbors = FindNeighbors(raid);
				if (neighbors.Count > 0)
				{
					neighbors.Add(raid);
					CreateBrigade(neighbors);
					i = Math.Max(0, i - neighbors.Count);
				}
			}
		}

		private void CreateBrigade(List<Raid> raids)
		{
			var brigade = new RaidBrigade(raids);
			for (var i = 0; i < raids.Count; i++)
			{
				EnterBrigade(raids[i]);
			}
			RegisterBrigade(brigade);
		}

		private List<Raid> FindNeighbors(Raid raid)
		{
			var neighbors = _singleRaids.FindAll(r => r.Position == raid.Position);
			neighbors.Remove(raid);

			return neighbors;
		}

		private void RaidRegistered(Raid raid)
		{
			_singleRaids.Add(raid);
		}

		private void EnterBrigade(Raid raid)
		{
			_singleRaids.Remove(raid);

			_raidManager.RaidEnterBrigade(raid);
		}

		private void LeaveBrigade(Raid raid)
		{
			_singleRaids.Add(raid);

			_raidManager.RaidLeaveBrigade(raid);
		}

		private void BrigadeSelected(RaidBrigade brigade, bool primary)
		{
			if (primary)
			{
				OnBrigadeSelected?.Invoke(brigade);
			}
		}

		private void BrigadeDisassemble(RaidBrigade brigade)
		{
			UnregisterBrigade(brigade);
		}
	}
}