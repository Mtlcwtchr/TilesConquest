using Tiles.Model;
using UnityEngine;

namespace Effects
{
	[CreateAssetMenu(fileName = "ModifyLoyaltyEffect", menuName = "Tiles/Effects/ModifyLoyalty", order = 0)]
	public class LoyaltyModifierEffect : TileEffect
	{
		[SerializeField] private float modifier;
		
		public override void Apply(TileFill source, World.World world)
		{
			source.Loyalty += modifier;
		}
	}
}