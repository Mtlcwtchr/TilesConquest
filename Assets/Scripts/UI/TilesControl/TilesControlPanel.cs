using Effects;
using Tiles.Manager;
using Tiles.Model;

namespace UI.TilesControl
{
	public class TilesControlPanel : UIModel<TilesControlPanelView>
	{
		private TilesManager _manager;

		private Tile _tile;
		private Tile Tile
		{
			get => _tile;
			set
			{
				_tile = value;
				_view.Tile = _tile;
			}
		}

		public TilesControlPanel(TilesControlPanelView view, TilesManager manager) : base(view)
		{
			_manager = manager;
			
			_manager.OnTileSelected += TileSelected;
			_view.OnClose += Close;
			_view.OnFocusSelected += FocusSelected;
		}

		private void FocusSelected(TileEffect effect)
		{
			Tile.Fill.ActiveFocus = effect;
			_view.UpdateFocus(Tile);
		}

		private void Close()
		{
			Hide();
		}

		private void TileSelected(Tile tile)
		{
			if (tile == null)
			{
				Hide();
				return;
			}

			Tile = tile;
			Show();
		}
	}
}