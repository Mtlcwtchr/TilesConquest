using Effects;
using TMPro;
using UI.ElementsList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Effects
{
	public class EffectElement : UIView, IListElement<TileEffect>
	{
		[SerializeField] private TMP_Text text;
		[SerializeField] private Image icon;

		public TileEffect Data
		{
			set
			{
				text.text = value.Description;
				icon.sprite = value.Icon;
			}
		}

	}
}