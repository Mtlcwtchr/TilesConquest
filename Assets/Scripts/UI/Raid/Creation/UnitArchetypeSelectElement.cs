using UI.Utils;
using Unit.Config;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation
{
	public class UnitArchetypeSelectElement : SelectableElement<UnitConfig>
	{
		[SerializeField] private Image icon;

		public override bool Selected { get; set; }

		protected override void UpdateContent()
		{
			icon.sprite = Data.icon;
		}
	}
}