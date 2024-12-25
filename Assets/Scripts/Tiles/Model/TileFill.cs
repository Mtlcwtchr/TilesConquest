using Tiles.Config;

namespace Tiles.Model
{
	public class TileFill
	{
		public TileFillConfig Config { get; }

		public TileFill(TileFillConfig config)
		{
			Config = config;
		}

		public void ApplyEffects(World.World world)
		{
			for (var i = 0; i < Config.effects.Count; i++)
			{
				Config.effects[i].Apply(world);
			}
		}
	}
}