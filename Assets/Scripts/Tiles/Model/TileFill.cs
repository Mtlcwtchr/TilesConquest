using Effects;
using Modifiers;
using Tiles.Config;
using UnityEngine;

namespace Tiles.Model
{
	public class TileFill
	{
		private float _loyalty;

		public float Loyalty
		{
			get => _loyalty;
			set => _loyalty = Mathf.Clamp(value, 0, 100);
		}

		public TileFillConfig Config { get; }
		
		public TileEffect ActiveFocus { get; set; }

		public TileFill(TileFillConfig config)
		{
			Config = config;
			Loyalty = 100;
		}

		public void ApplyEffects(World.World world)
		{
			for (var i = 0; i < Config.effects.Count; i++)
			{
				Config.effects[i].Apply(this, world);
			}

			ActiveFocus?.Apply(this, world);
		}

		public void ApplyModifier(IModifier modifier)
		{
			
		}

		public void RemoveModifier(IModifier modifier)
		{
			
		}
	}
}