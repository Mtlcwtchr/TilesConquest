using System;
using System.Linq;
using Game;
using UI.Utils;
using Unit.Raid.Brigade;
using UnityEngine;

namespace UI.Raid.Brigade
{
	public class BrigadeInfoPanelView : UIView
	{
		public event Action<Unit.Raid.Raid> OnRaidSelected;
		public event Action<Player> OnPlayerSelected; 

		[SerializeField] private PlayerSelectElement playerSelectElement;
		[SerializeField] private Transform playerElementsRoot;
		
		[SerializeField] private RaidInfoElement raidInfoElement;
		[SerializeField] private Transform raidElementsRoot;
		[SerializeField] private Transform enemyRaidElementsRoot;

		private SelectableElementsList<Player> _playerElements;
		private SelectableElementsList<Unit.Raid.Raid> _raidElements;
		private SelectableElementsList<Unit.Raid.Raid> _enemyRaidElements;

		private BrigadeInfoPanel _model;
		
		private RaidBrigade _brigade;

		public RaidBrigade Brigade
		{
			get => _brigade;
			set
			{
				_brigade = value;
				_raidElements.Data = Brigade.Get(GameManager.Instance.Player);
				_playerElements.Data = Brigade.Players.Where(p => p != GameManager.Instance.Player).ToList();
			}
		}

		private Player _selectedPlayer;
		public Player SelectedPlayer
		{
			get => _selectedPlayer;
			set
			{
				_selectedPlayer = value;
				_enemyRaidElements.Data = Brigade.Get(SelectedPlayer);
			}
		}

		public void Init(BrigadeInfoPanel model)
		{
			_model = model;
			
			_raidElements = new SelectableElementsList<Unit.Raid.Raid>(raidInfoElement, raidElementsRoot);
			_enemyRaidElements = new SelectableElementsList<Unit.Raid.Raid>(raidInfoElement, enemyRaidElementsRoot);
			_playerElements = new SelectableElementsList<Player>(playerSelectElement, playerElementsRoot);
			
			_raidElements.OnSelect += ElementSelected;
			_playerElements.OnSelect += PlayerSelected;
		}

		private void PlayerSelected(SelectableElement<Player> player)
		{
			OnPlayerSelected?.Invoke(player.Data);
		}

		private void ElementSelected(SelectableElement<Unit.Raid.Raid> raidElement)
		{
			OnRaidSelected?.Invoke(raidElement.Data);
		}
	}
}