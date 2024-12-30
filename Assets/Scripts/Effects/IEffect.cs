using Tiles.Model;

namespace Effects
{
	public interface IEffect
	{
		public void Apply(TileFill source, World.World world);
	}
}