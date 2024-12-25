using Tiles.Manager;
using Tiles.Model;

namespace UI.TilesInfo
{
	public class TilesInfoPanel
	{
		private TilesInfoPanelView _view;
		private TilesManager _manager;

		public TilesInfoPanel(TilesInfoPanelView view, TilesManager manager)
		{
			_view = view;
			_manager = manager;
			
			_manager.OnFillSelected += FillSelected;
		}

		private void FillSelected(TileFill fill)
		{
			if (fill == null)
			{
				Hide();
				return;
			}
			
			_view.Tile = fill;
			Show();
		}

		public void Show()
		{
			_view.Show();
		}

		public void Hide()
		{
			_view.Hide();
		}
	}
}