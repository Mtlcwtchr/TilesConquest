using System.Linq;
using Game;
using Unit.Raid.Brigade;

namespace UI.Raid.Brigade
{
	public class BrigadeInfoPanel : UIModel<BrigadeInfoPanelView>
	{
		private BrigadeInfoPanelView _view;
		
		public BrigadeInfoPanel(BrigadeInfoPanelView view, RaidBrigadeManager raidBrigadeManager) : base(view)
		{
			_view = view;
			_view.OnRaidSelected += RaidSelected;
			_view.OnPlayerSelected += PlayerSelected;
			
			raidBrigadeManager.OnBrigadeSelected += BrigadeSelected;
		}

		private void PlayerSelected(Player player)
		{
			_view.SelectedPlayer = player;
		}

		private void BrigadeSelected(RaidBrigade brigade)
		{
			Show();
			_view.Brigade = brigade;
			_view.SelectedPlayer = brigade.Players.First(p => p != GameManager.Instance.Player);
		}

		private void RaidSelected(Unit.Raid.Raid raid)
		{
			Hide();
			raid.Select(true);
		}
	}
}