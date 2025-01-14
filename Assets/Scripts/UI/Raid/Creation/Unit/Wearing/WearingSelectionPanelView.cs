using System;
using System.Collections.Generic;
using UI.Utils;
using Unit.Wearing;
using UnityEngine;

namespace UI.Raid.Creation.Unit.Wearing
{
	public class WearingSelectionPanelView : UIView
	{
		public event Action<EWearingSlot> OnSlotSelected;
		public event Action<IWearing> OnWearingSelected; 
		
		[SerializeField] private Transform elementsRoot;
		[SerializeField] private WearingSelectElement element;
		[SerializeField] private List<WearingSlotSelectElement> slotSelections;

		private SelectableElementsList<IWearing> _wearingsList;
		
		public List<IWearing> Wearings
		{
			set => _wearingsList.Data = value;
		}

		public EWearingSlotMask AvailableSlots
		{
			set
			{
				for (var i = 0; i < slotSelections.Count; i++)
				{
					if ((value & (EWearingSlotMask)(1 << (int)slotSelections[i].Slot)) > 0)
					{
						slotSelections[i].Show();
					}
					else
					{
						slotSelections[i].Hide();
					}
				}
			}
		}

		public void Init()
		{
			_wearingsList = new SelectableElementsList<IWearing>(element, elementsRoot);
			_wearingsList.OnSelect += WearingSelected;
			
			for (var i = 0; i < slotSelections.Count; i++)
			{
				slotSelections[i].OnSlotSelected += SlotSelected;
			}
		}

		private void OnDestroy()
		{
			for (var i = 0; i < slotSelections.Count; i++)
			{
				slotSelections[i].OnSlotSelected -= SlotSelected;
			}
		}

		public void UpdateSelection(IWearing wearing)
		{
			for (var i = 0; i < _wearingsList.Elements.Count; i++)
			{
				_wearingsList.Elements[i].Selected = _wearingsList.Elements[i].Data == wearing;
			}
		}

		private void SlotSelected(EWearingSlot slot)
		{
			OnSlotSelected?.Invoke(slot);
		}

		private void WearingSelected(SelectableElement<IWearing> element)
		{
			OnWearingSelected?.Invoke(element.Data);
		}
	}
}