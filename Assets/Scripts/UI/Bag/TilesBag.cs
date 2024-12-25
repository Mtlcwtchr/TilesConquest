using Tiles.Manager;
using Tiles.Model;
using Tiles.Pool;
using UnityEngine;

namespace UI.Bag
{
	public class TilesBag : UIModel<TilesBagView>
	{
		private TilesPool _pool;
		private TilesManager _manager;

		private bool _locked;
		public override bool Locked
		{
			get => _locked;
			set
			{
				_locked = value;
				if (_locked)
				{
					Hide();
				}
				else
				{
					Show();
				}
			}
		}

		public TilesBag(TilesBagView view, TilesPool pool, TilesManager manager) : base(view)
		{
			_pool = pool;
			_manager = manager;
			
			_manager.OnFillSelected += FillSelected;
			_view.OnButtonClick += Click;
		}

		private void FillSelected(TileFill fill)
		{
			_view.Enabled = fill == null;
		}

		private void Click()
		{
			if (Locked)
			{
				ClickOnLocked();
				return;
			}
			
			var tileConfig = _pool.Get();
			var tileFill = new TileFill(tileConfig);
			_manager.NotifyFillSelected(tileFill);
		}

		private void ClickOnLocked()
		{
			Debug.Log("Can not do that");
		}
	}
}