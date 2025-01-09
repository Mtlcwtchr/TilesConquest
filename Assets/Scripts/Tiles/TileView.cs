using System;
using Game;
using Tiles.Model;
using UnityEngine;

namespace Tiles
{
	public class TileView : MonoBehaviour, ISelectable
	{
		public event Action<bool> OnSelect;
		
		[SerializeField] private GameObject highlight;
		
		private Tile _model;

		private GameObject _visual;
        
		public bool Highlighted { get => highlight.activeSelf; set => highlight.SetActive(value); }
		
		public void Init(Tile model)
		{
			_model = model;
			_model.BindView(this);
		}

		public void UpdateFill(TileFill fill)
		{
			var viewPattern = fill.Config.fillView;
			_visual = Instantiate(viewPattern, transform);
		}

		public void Select(bool primary)
		{
			OnSelect?.Invoke(primary);
		}
	}
}