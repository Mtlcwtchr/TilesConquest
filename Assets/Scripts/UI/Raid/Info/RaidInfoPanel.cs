using Unit.Raid;

namespace UI.Raid.Info
{
	public class RaidInfoPanel : UIModel<RaidInfoPanelView>
	{
		private RaidManager _raidManager;
		
		public RaidInfoPanel(RaidInfoPanelView view, RaidManager raidManager) : base(view)
		{
			_view.OnClose += Close;
			
			_raidManager = raidManager;
			_raidManager.OnRaidSelected += RaidSelected;
		}

		private void Close()
		{
			Hide();
		}

		private void RaidSelected(Unit.Raid.Raid raid)
		{
			if (raid == null)
			{
				_view.Hide();
			}
			
			_view.Show();
			_view.Raid = raid;
		}
	}
}