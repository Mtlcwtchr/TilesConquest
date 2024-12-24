﻿using Tiles.Manager;
using Tiles.Model;
using Tiles.Pool;

namespace UI.Bag
{
	public class TilesBag
	{
		private TilesPool _pool;
		private TilesManager _manager;

		private TilesBagView _view;
		
		public TilesBag(TilesPool pool, TilesManager manager)
		{
			_pool = pool;
			_manager = manager;
			
			_manager.OnFillSelected += FillSelected;
		}

		private void FillSelected(TileFill fill)
		{
			_view.Enabled = fill == null;
		}

		public void BindView(TilesBagView view)
		{
			_view = view;
			
			_view.OnButtonClick += Click;
		}

		private void Click()
		{
			var tileConfig = _pool.Get();
			var tileFill = new TileFill(tileConfig);
			_manager.NotifyFillSelected(tileFill);
		}
	}
}