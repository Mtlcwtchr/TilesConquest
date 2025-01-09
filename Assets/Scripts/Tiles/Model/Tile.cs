using System;
using UnityEngine;

namespace Tiles.Model
{
	public class Tile
	{
		public event Action<Tile, bool> OnSelect; 
		
		public bool Filled { get; private set; }

		public TileFill Fill => _fill;
		
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
			_view.OnSelect += Select;
		}

		public void SetFill(TileFill fill)
		{
			_fill = fill;
			Filled = true;

			_view.UpdateFill(_fill);
		}

		public void ApplyEffects(World.World world)
		{
			_fill?.ApplyEffects(world);
		}

		private void Select(bool primary)
		{
			OnSelect?.Invoke(this, primary);
		}
	}
}