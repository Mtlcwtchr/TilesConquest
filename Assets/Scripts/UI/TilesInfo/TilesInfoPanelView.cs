using Tiles.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TilesInfo
{
	public class TilesInfoPanelView : MonoBehaviour
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private TMP_Text description;
		[SerializeField] private Image image;

		public TileFill Tile
		{
			set
			{
				var config = value.Config;
				label.text = config.tileName;
				description.text = config.tileDescription;
				image.sprite = config.tileImage;
			}
		}
		
		public void Init(TilesInfoPanel model) {}
		
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}