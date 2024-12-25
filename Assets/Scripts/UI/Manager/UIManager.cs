using System;
using Game;
using UI.Bag;

namespace UI.Manager
{
	public class UIManager
	{
		private Player _player;
		
		private TilesBag _bag;
		
		public UIManager(Player player, TilesBag bag)
		{
			_player = player;
			_bag = bag;
			
			_player.OnTurn += PlayerTurn;
		}

		private void PlayerTurn(ETurnPhase phase)
		{
			switch (phase)
			{
				case ETurnPhase.PlaceTile:
					_bag.Locked = false;
					break;
				case ETurnPhase.Decide:
					_bag.Locked = true;
					break;
			}
		}
	}
}