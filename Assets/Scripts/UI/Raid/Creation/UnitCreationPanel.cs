using Unit.Config;
using Unit.Creation;
using Unit.Wearing;

namespace UI.Raid.Creation
{
	public class UnitCreationPanel : UIModel<UnitCreationPanelView>
	{
		private UnitCreationPanelView _view;

		public UnitTemplate Template
		{
			get => _view.Template;
			set => _view.Template = value;
		}
		
		public UnitCreationPanel(UnitCreationPanelView view) : base(view)
		{
			_view = view;
			
			_view.OnButtonClick += TestButtonClick;
		}

		private void TestButtonClick(WearingConfig wearingConfig)
		{
			var sword = new TestSword(wearingConfig);
			if (Template.Wearings.TryGetValue(wearingConfig.slot, out var value) && value != null)
			{
				Template.UnEquipCurrent(wearingConfig.slot);
				return;
			}
			Template.TryEquip(sword);
		}
	}
}