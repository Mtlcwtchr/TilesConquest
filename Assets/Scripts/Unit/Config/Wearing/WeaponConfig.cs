using Unit.Wearing;
using UnityEngine;

namespace Unit.Config.Wearing
{
	[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Units/WeaponConfig")]
	public class WeaponConfig : WearingConfig
	{
		public int damage;
		
		public override IWearing Create()
		{
			return new WeaponWearing(this);
		}
	}
}