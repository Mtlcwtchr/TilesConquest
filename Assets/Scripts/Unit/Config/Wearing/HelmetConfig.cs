using Unit.Wearing;
using UnityEngine;

namespace Unit.Config.Wearing
{
	[CreateAssetMenu(fileName = "HelmetConfig", menuName = "Units/HelmetConfig")]
	public class HelmetConfig : WearingConfig
	{
		public int defence;
		
		public override IWearing Create()
		{
			return new HelmetWearing(this);
		}
	}
}