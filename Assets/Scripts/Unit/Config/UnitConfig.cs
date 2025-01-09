using Unit.Visual;
using Unit.Wearing;
using UnityEngine;

namespace Unit.Config
{
	[CreateAssetMenu(fileName = "UnitConfig", menuName = "Units/UnitConfig")]
	public class UnitConfig : ScriptableObject
	{
		public string title;
		public string description;
		public int hp;
		public int damage;
		public int speed;
		public int priority;
		
		public UnitVisualisation visualBase;
		public EWearingSlotMask availableWearings;
	}
}