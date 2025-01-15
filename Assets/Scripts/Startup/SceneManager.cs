using Game;
using Tiles;
using Tiles.Config;
using Tiles.Manager;
using Tiles.Pool;
using UI.Bag;
using UI.Manager;
using UI.Raid.Brigade;
using UI.Raid.Creation;
using UI.Raid.Creation.Unit;
using UI.Raid.Creation.Unit.Wearing;
using UI.Raid.Info;
using UI.Storage;
using UI.TilesControl;
using UI.TilesInfo;
using UI.Turn;
using Unit.Config;
using Unit.Creation;
using Unit.Raid;
using Unit.Raid.Brigade;
using UnityEngine;
using UnityEngine.UI;
using World;
using World.Era;
using World.Era.Config;
using World.Units;

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

		[SerializeField] private UnitCreationPanelView creationPanel;

		[SerializeField] private UnitConfig unit;

		[SerializeField] private EraConfig eraConfig;
		[SerializeField] private WearingSelectionPanelView panelView;

		[SerializeField] private RaidCreationPanelView raidCreationView;

		[SerializeField] private Button test;
		[SerializeField] private Button test2;

		[SerializeField] private RaidView raid;
		[SerializeField] private RaidBrigadeView brigadeView;

		[SerializeField] private RaidInfoPanelView raidInfoPanel;

		[SerializeField] private BrigadeInfoPanelView brigadeInfoPanelView;

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

			var raidManager = new RaidManager(raid);
			var raidBrigadeManager = new RaidBrigadeManager(raidManager, brigadeView);

			var era = new Era(eraConfig);

			var storage = new Storage(EResource.Gold, EResource.Goods, EResource.Food, EResource.Recruits);
			var forge = new Forge(era);
			var recruitHouse = new RecruitHouse(era);
			_world = new World.World(era, storage, forge, recruitHouse, manager, raidManager, raidBrigadeManager);
			raidManager.SetWorld(_world);

			var storagePanelModel = new StoragePanel(storagePanel, storage);
			storagePanel.Init(storagePanelModel);

			var infoPanelModel = new TilesInfoPanel(infoPanel, manager);
			infoPanel.Init(infoPanelModel);
			infoPanelModel.Hide();

			var tilesControlPanelModel = new TilesControlPanel(tilesControlPanel, manager);
			tilesControlPanel.Hide();

			var player = new Player(manager);
			var player2 = new Player(manager);

			turnControl.Init(player);
			
			_gameManager = new GameManager(new() { player }, _world);
			_gameManager.Player = player;
			_gameManager.OnTurnFinish += UpdateTurn;

			_uiManager = new UIManager(player, bag, tilesControlPanelModel);

			var wearingModel = new WearingSelectionPanel(panelView);
			panelView.Init();
			var creationModel = new UnitCreationPanel(creationPanel, wearingModel, _world);
			creationPanel.Init(creationModel);

			var raidCreation = new RaidCreationPanel(raidCreationView, creationModel, raidManager);
			raidCreationView.Init();

			var raidInfoModel = new RaidInfoPanel(raidInfoPanel, raidManager);
			raidInfoPanel.Init(raidInfoModel);

			var brigadeInfoModel = new BrigadeInfoPanel(brigadeInfoPanelView, raidBrigadeManager);
			brigadeInfoPanelView.Init(brigadeInfoModel);
			
			test.onClick.AddListener(() =>
			{
				raidCreation.RaidTemplate = new RaidTemplate(player);
				raidCreation.Archetypes = _world.RecruitHouse.Archetypes;
				raidCreation.Position = manager.GetCapital().Position;
				raidCreation.Show();
			});
			
			test2.onClick.AddListener(() =>
			{
				raidCreation.RaidTemplate = new RaidTemplate(player2);
				raidCreation.Archetypes = _world.RecruitHouse.Archetypes;
				raidCreation.Position = manager.GetCapital().Position;
				raidCreation.Show();
			});
			
			UpdateTurn();
		}

		private void UpdateTurn()
		{
			_gameManager.Update();
		}
	}
}