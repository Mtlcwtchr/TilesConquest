using System;
using UnityEngine;

namespace Tiles.Model
{
	public class Tile
	{
		public event Action<Tile> OnMouseClick; 
		
		public bool Filled { get; private set; }
		
		public Vector2Int Position { get; }
		
		public bool Highlighted { 
			get => _view.Highlighted;
			set => _view.Highlighted = value;
		}
		
		public bool Available { get; set; }

		private TileFill _fill;
		
		private TileView _view;

		public Tile(Vector2Int position)
		{
			Position = position;
		}

		public void BindView(TileView view)
		{
			_view = view;
			_view.OnMouseClick += MouseClick;
		}

		public void SetFill(TileFill fill)
		{
			_fill = fill;
			Filled = true;

			_view.UpdateFill(_fill);
		}

		private void MouseClick()
		{
			OnMouseClick?.Invoke(this);
		}
	}
}