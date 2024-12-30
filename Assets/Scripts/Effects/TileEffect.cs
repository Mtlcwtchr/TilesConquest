using Tiles.Model;
using UnityEngine;

namespace Effects
{
	public abstract class TileEffect : ScriptableObject, IEffect
	{
		[SerializeField] private string description;
		[SerializeField] private Sprite icon;

		public virtual string Description => description;
		public Sprite Icon => icon;

		public abstract void Apply(TileFill source, World.World world);
	}
}