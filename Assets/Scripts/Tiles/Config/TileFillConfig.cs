using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Tiles.Config
{
	[CreateAssetMenu(fileName = "FillConfig", menuName = "Tiles/FillConfig", order = 0)]
	public class TileFillConfig : ScriptableObject
	{
		public string tileName;
		public string tileDescription;
		public Sprite tileImage;
		
		public GameObject fillView;

		public List<TileEffect> effects;
	}
}