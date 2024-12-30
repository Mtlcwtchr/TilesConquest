using System;
using System.Collections.Generic;
using Effects;
using Tiles.Model;
using TMPro;
using UI.ElementsList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TilesControl
{
	public class TilesControlPanelView : UIView
	{
		public event Action OnClose;
		public event Action<TileEffect> OnFocusSelected; 

		[Serializable]
		private class FocusButton
		{
			public Button button;
			public TileEffect effect;
		}
		
		[SerializeField] private TMP_Text label;
		[SerializeField] private Image image;
		[SerializeField] private TMP_Text loyalty;
		[SerializeField] private Button closeButton;
		
		[SerializeField] private List<FocusButton> focusButtons;

		[SerializeField] private EffectsList effectsList;
		
		public Tile Tile
		{
			set
			{
				var config = value.Fill.Config;
				label.text = config.tileName;
				image.sprite = config.tileImage;
				loyalty.text = value.Fill.Loyalty.ToString(".#");

				effectsList.RenderList(config.effects);

				UpdateFocus(value);
			}
		}

		public void UpdateFocus(Tile tile)
		{
			
			for (var i = 0; i < focusButtons.Count; i++)
			{
				focusButtons[i].button.interactable = tile.Fill.ActiveFocus != focusButtons[i].effect;
			}
		}

		private void Awake()
		{
			closeButton.onClick.AddListener(Close);
		}

		private void OnDestroy()
		{
			closeButton.onClick.RemoveListener(Close);
		}

		public void SelectFocus(int i)
		{
			OnFocusSelected?.Invoke(focusButtons[i].effect);
		}

		private void Close()
		{
			OnClose?.Invoke();
		}
		
	}
}