using Unit.Wearing;
using UnityEngine;

namespace Unit.Config.Wearing
{
	[CreateAssetMenu(fileName = "ArmorConfig", menuName = "Units/ArmorConfig")]
	public class ArmorConfig : WearingConfig
	{
		public int defence;
		
		public override IWearing Create()
		{
			return new ArmorWearing(this);
		}
	}
}