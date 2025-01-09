using System;
using Tiles.Manager;
using Tiles.Model;

namespace Game
{
	public class Player
	{
		public event Action<ETurnPhase> OnTurn;
		
		public event Action OnTurnFinish;

		private TilesManager _tilesManager;
		
		public Player(TilesManager tilesManager)
		{
			_tilesManager = tilesManager;
			
			_tilesManager.OnFillSelected += FillSelected;
			
		}

		private void FillSelected(TileFill fill)
		{
			if (fill == null)
			{
				OnTurn?.Invoke(ETurnPhase.Decide);
			}
		}

		public void FinishTurn()
		{
			OnTurnFinish?.Invoke();
		}

		public void NotifyTurn()
		{
			OnTurn?.Invoke(ETurnPhase.PlaceTile);
		}
	}
}