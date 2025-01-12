using System;
using System.Collections.Generic;
using Unit.Wearing;

namespace UI.Raid.Creation.Wearing
{
	public class WearingSelectionPanel : UIModel<WearingSelectionPanelView>
	{
		public event Action<IWearing> OnWearingSelected;
		public event Action<EWearingSlot> OnSlotSelected; 

		public EWearingSlotMask AvailableSlots
		{
			set => _view.AvailableSlots = value;
		}

		public List<IWearing> Wearings
		{
			set => _view.Wearings = value;
		}
		
		public WearingSelectionPanel(WearingSelectionPanelView view) : base(view)
		{
			_view.OnSlotSelected += SlotSelected;
			_view.OnWearingSelected += WearingSelected;
		}

		public void UpdateSelection(IWearing wearing)
		{
			_view.UpdateSelection(wearing);
		}

		private void SlotSelected(EWearingSlot slot)
		{
			OnSlotSelected?.Invoke(slot);
		}

		private void WearingSelected(IWearing wearing)
		{
			OnWearingSelected?.Invoke(wearing);
		}
	}
}