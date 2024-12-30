using System;
using Game;
using UI.Bag;
using UI.TilesControl;

namespace UI.Manager
{
	public class UIManager
	{
		private Player _player;
		
		private TilesBag _bag;

		private TilesControlPanel _controlPanel;
		
		public UIManager(Player player, TilesBag bag, TilesControlPanel controlPanel)
		{
			_player = player;
			_bag = bag;
			_controlPanel = controlPanel;
			
			_player.OnTurn += PlayerTurn;
			_player.OnTurnFinish += PlayerTurnFinish;
		}

		private void PlayerTurnFinish()
		{
			_controlPanel.Locked = true;
		}

		private void PlayerTurn(ETurnPhase phase)
		{
			switch (phase)
			{
				case ETurnPhase.PlaceTile:
					_bag.Locked = false;
					_controlPanel.Locked = false;
					break;
				case ETurnPhase.Decide:
					_bag.Locked = true;
					break;
			}
		}
	}
}