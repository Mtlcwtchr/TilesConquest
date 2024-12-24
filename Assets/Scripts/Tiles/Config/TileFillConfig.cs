using UnityEngine;

namespace Tiles.Config
{
	[CreateAssetMenu(fileName = "FillConfig", menuName = "Tiles/FillConfig", order = 0)]
	public class TileFillConfig : ScriptableObject
	{
		public GameObject fillView;
	}
}