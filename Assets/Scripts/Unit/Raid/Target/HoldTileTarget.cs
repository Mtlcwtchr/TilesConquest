using Modifiers;
using Tiles.Model;

namespace Unit.Raid.Target
{
	public class HoldTileTarget : Target
	{
		private IModifier _holdModifier;
		private Tile _holdingTile;

		public HoldTileTarget(Raid raid, Tile tile) : base(raid)
		{
			_holdingTile = tile;
		}
		
		public override void Start()
		{
			_holdingTile.Fill.ApplyModifier(_holdModifier);
		}

		public override void Advance() { }

		public override void Cancel()
		{
			_holdingTile.Fill.RemoveModifier(_holdModifier);
			
			base.Cancel();
		}
	}
}