using TMPro;
using UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Brigade
{
	public class RaidInfoElement : SelectableElement<Unit.Raid.Raid>
	{
		[SerializeField] private Image image;
		[SerializeField] private TMP_Text raidTitle;
		
		[SerializeField] private TMP_Text hp;
		[SerializeField] private TMP_Text damage;
		[SerializeField] private Image hpBar;

		public override bool Selected { get; set; }
		
		protected override void UpdateContent()
		{
			hp.text = $"{Data.Hp}/{Data.MaxHp}";
			damage.text = Data.Damage.ToString();
			hpBar.fillAmount = Data.RelativeHp;
		}
	}
}