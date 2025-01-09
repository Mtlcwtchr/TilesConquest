using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Info
{
	public class RaidInfoPanelView : UIView
	{
		[SerializeField] private TMP_Text hp;
		[SerializeField] private TMP_Text damage;
		[SerializeField] private TMP_Text speed;
		[SerializeField] private Image hpBar;

		[SerializeField] private List<UnitInfoElement> unitElements;

		public Unit.Raid.Raid Raid
		{
			set
			{
				hp.text = $"{value.Hp}/{value.MaxHp}";
				damage.text = value.Damage.ToString();
				speed.text = value.Speed.ToString();
				hpBar.fillAmount = value.RelativeHp;

				DrawUnits(value.Units);
			}
		}

		private void DrawUnits(List<Unit.Unit> units)
		{
			for (var i = 0; i < units.Count; i++)
			{
				unitElements[i].Unit = units[i];
				unitElements[i].Show();
			}
			
			for (var i = units.Count; i < unitElements.Count; i++)
			{
				unitElements[i].Hide();
			}
		}
	}
}