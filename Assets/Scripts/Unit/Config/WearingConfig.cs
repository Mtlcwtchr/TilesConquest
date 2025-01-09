using Unit.Wearing;
using UnityEngine;

namespace Unit.Config
{
	[CreateAssetMenu(fileName = "WearingConfig", menuName = "Units/WearingConfig")]

	public class WearingConfig : ScriptableObject
	{
		public string title;
		public string description;
		
		public GameObject visual;
		public EWearingSlot slot;
	}
}