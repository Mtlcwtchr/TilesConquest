using Game;
using Tiles;
using Tiles.Config;
using Tiles.Manager;
using Tiles.Pool;
using UI.Bag;
using UI.Manager;
using UI.Storage;
using UI.TilesControl;
using UI.TilesInfo;
using UI.Turn;
using Unit.Raid;
using UnityEngine;
using World;

namespace Startup
{
	public class SceneManager : MonoBehaviour
	{
		[SerializeField] private TilesManagerView managerView;
		[SerializeField] private TilesPoolConfig poolConfig;

		[SerializeField] private TilesBagView bagView;
		[SerializeField] private float updateTime;

		[SerializeField] private StoragePanelView storagePanel;

		[SerializeField] private TilesInfoPanelView infoPanel;

		[SerializeField] private TurnControl turnControl;

		[SerializeField] private TilesControlPanelView tilesControlPanel;

		[SerializeField] private RaidView raid;

		private float _deltaTime;

		private World.World _world;

		private GameManager _gameManager;

		private UIManager _uiManager;
		
		private void Awake()
		{
			var gridSize = new Vector2Int(10, 10);
			var manager = new TilesManager();
			managerView.Init(manager);
			manager.CreateGrid(gridSize);

			var pool = new TilesPool(poolConfig);
			var bag = new TilesBag(bagView, pool, manager);
			bagView.Init(bag);

			var raidManager = new RaidManager();

			var storage = new Storage(EResource.Gold, EResource.Goods, EResource.Food, EResource.Recruits);
			_world = new World.World(storage, manager, raidManager);
			raidManager.SetWorld(_world);

			var storagePanelModel = new StoragePanel(storagePanel, storage);
			storagePanel.Init(storagePanelModel);

			var infoPanelModel = new TilesInfoPanel(infoPanel, manager);
			infoPanel.Init(infoPanelModel);
			infoPanelModel.Hide();

			var tilesControlPanelModel = new TilesControlPanel(tilesControlPanel, manager);
			tilesControlPanel.Hide();

			var player = new Player(manager);

			turnControl.Init(player);
			
			_gameManager = new GameManager(new() { player }, _world);
			_gameManager.OnTurnFinish += UpdateTurn;

			_uiManager = new UIManager(player, bag, tilesControlPanelModel);
			
			var raidModel = new Raid(player);
			raidManager.RegisterRaid(raidModel);
			raid.Init(raidModel, raidManager);
			
			UpdateTurn();
		}

		private void UpdateTurn()
		{
			_gameManager.Update();
		}
	}
}