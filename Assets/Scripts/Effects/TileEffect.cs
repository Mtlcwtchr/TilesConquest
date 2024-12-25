using UnityEngine;

namespace Effects
{
	public abstract class TileEffect : ScriptableObject, IEffect
	{
		[SerializeField] private string description;
		[SerializeField] private Sprite icon;

		public string Description => description;
		public Sprite Icon => icon;

		public abstract void Apply(World.World world);
	}
}