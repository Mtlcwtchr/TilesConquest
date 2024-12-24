using Tiles;
using Tiles.Config;
using Tiles.Manager;
using Tiles.Pool;
using UI.Bag;
using UnityEngine;

namespace Startup
{
	public class Startup : MonoBehaviour
	{
		[SerializeField] private TilesManagerView managerView;
		[SerializeField] private TilesPoolConfig poolConfig;

		[SerializeField] private TilesBagView bagView;
		
		private void Awake()
		{
			var gridSize = new Vector2Int(10, 10);
			var manager = new TilesManager();
			managerView.Init(manager);
			manager.CreateGrid(gridSize);

			var pool = new TilesPool(poolConfig);
			var bag = new TilesBag(pool, manager);
			bagView.Init(bag);
		}
	}
}