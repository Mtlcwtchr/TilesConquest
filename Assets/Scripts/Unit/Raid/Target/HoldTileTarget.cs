using Modifiers;
using Tiles.Model;

namespace Unit.Raid.Target
{
	public class HoldTileTarget : ITarget
	{
		private IModifier _holdModifier;
		private Tile _holdingTile;
		private Raid _raid;

		public void Start()
		{
			_holdingTile.Fill.ApplyModifier(_holdModifier);
		}

		public void Advance() { }

		public void Cancel()
		{
			_holdingTile.Fill.RemoveModifier(_holdModifier);
		}
	}
}