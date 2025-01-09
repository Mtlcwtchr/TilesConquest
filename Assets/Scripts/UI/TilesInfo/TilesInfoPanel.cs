using Tiles.Manager;
using Tiles.Model;

namespace UI.TilesInfo
{
	public class TilesInfoPanel : UIModel<TilesInfoPanelView>
	{
		private TilesManager _manager;

		public TilesInfoPanel(TilesInfoPanelView view, TilesManager manager) : base(view)
		{
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
	}
}