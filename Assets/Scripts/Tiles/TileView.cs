using System;
using Tiles.Model;
using UnityEngine;

namespace Tiles
{
	public class TileView : MonoBehaviour
	{
		public event Action OnMouseClick;
		
		[SerializeField] private GameObject highlight;
        
		public bool Highlighted { get => highlight.activeSelf; set => highlight.SetActive(value); }
		
		private Tile _model;
		
		public void Init(Tile model)
		{
			_model = model;
			_model.BindView(this);
		}

		public void UpdateFill(TileFill fill)
		{
			var viewPattern = fill.Config.fillView;
			var view = Instantiate(viewPattern, transform);
		}

		private void OnMouseUpAsButton()
		{
			OnMouseClick?.Invoke();
		}
	}
}