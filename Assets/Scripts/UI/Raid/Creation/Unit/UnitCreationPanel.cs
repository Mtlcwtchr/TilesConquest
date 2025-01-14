using System;
using UI.Raid.Creation.Unit.Wearing;
using Unit.Creation;
using Unit.Wearing;

namespace UI.Raid.Creation.Unit
{
	public class UnitCreationPanel : UIModel<UnitCreationPanelView>
	{
		public event Action<UnitTemplate> OnCreationApplied; 
		
		private WearingSelectionPanel _wearingPanel;
		
		private World.World _world;

		public UnitTemplate Template
		{
			get => _view.Template;
			set
			{
				_view.Template = value;
				_wearingPanel.AvailableSlots = value.Config.availableWearings;
			}
		}
		
		public UnitCreationPanel(UnitCreationPanelView view, WearingSelectionPanel panel, World.World world) : base(view)
		{
			_wearingPanel = panel;
			_world = world;
			
			_wearingPanel.OnWearingSelected += WearingSelected;
			_wearingPanel.OnSlotSelected += SlotSelected;
			
			_view.OnApply += Apply;
			_view.OnClose += Close;
		}

		private void SlotSelected(EWearingSlot slot)
		{
			var wearings = _world.Forge.Get(slot);
			_wearingPanel.Wearings = wearings;
			
			_wearingPanel.UpdateSelection(Template.GetEquipped(slot));
		}

		private void WearingSelected(IWearing wearing)
		{
			if (Template.Equipped(wearing))
			{
				Template.UnEquipCurrent(wearing.Slot);
				_wearingPanel.UpdateSelection(null);
			}
			else
			{
				var equipped = Template.TryEquip(wearing);
				_wearingPanel.UpdateSelection(equipped ? wearing : null);
			}
		}

		public override void Show()
		{
			SlotSelected(EWearingSlot.HandL);
			base.Show();
		}

		private void Apply()
		{
			Hide();
			OnCreationApplied?.Invoke(Template);
		}

		private void Close()
		{
			Hide();
		}
	}
}