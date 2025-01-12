using Unit.Wearing;
using UnityEngine;

namespace Unit.Config.Wearing
{
	[CreateAssetMenu(fileName = "WearingConfig", menuName = "Units/WearingConfig")]

	public abstract class WearingConfig : ScriptableObject
	{
		public string title;
		public string description;
		public Sprite icon;
		
		public GameObject visual;
		public EWearingSlot slot;

		public abstract IWearing Create();
	}
}