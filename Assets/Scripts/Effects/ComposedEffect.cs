using System.Collections.Generic;
using Tiles.Model;
using UnityEngine;

namespace Effects
{
	[CreateAssetMenu(fileName = "ComposedEffect", menuName = "Tiles/Effects/Composed", order = 0)]
	public class ComposedEffect : TileEffect
	{
		[SerializeField] private List<TileEffect> effects;
		
		public override void Apply(TileFill source, World.World world)
		{
			for (var i = 0; i < effects.Count; i++)
			{
				effects[i].Apply(source, world);
			}
		}
	}
}