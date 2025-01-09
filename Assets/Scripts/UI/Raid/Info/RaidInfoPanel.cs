using Unit.Raid;

namespace UI.Raid.Info
{
	public class RaidInfoPanel : UIModel<RaidInfoPanelView>
	{
		private RaidManager _raidManager;
		
		public RaidInfoPanel(RaidInfoPanelView view, RaidManager raidManager) : base(view)
		{
			_raidManager = raidManager;
			_raidManager.OnRaidSelected += RaidSelected;
		}

		private void RaidSelected(Unit.Raid.Raid raid)
		{
			if (raid == null)
			{
				_view.Hide();
			}
			
			_view.Raid = raid;
			_view.Show();
		}
	}
}