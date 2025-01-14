using UI.Utils;
using Unit.Wearing;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation.Unit.Wearing
{
	public class WearingSelectElement : SelectableElement<IWearing>
	{
		[SerializeField] private Image icon;
		[SerializeField] private GameObject selection;

		private bool _selected;
		public override bool Selected
		{
			get => _selected;
			set
			{
				_selected = value;
				selection.SetActive(_selected);
			}
		}

		protected override void UpdateContent()
		{
			icon.sprite = Data.Config.icon;
		}

	}
}