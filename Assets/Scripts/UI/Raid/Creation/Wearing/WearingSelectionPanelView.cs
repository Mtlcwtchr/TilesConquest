using System;
using System.Collections.Generic;
using Unit.Wearing;
using UnityEngine;

namespace UI.Raid.Creation.Wearing
{
	public class WearingSelectionPanelView : UIView
	{
		public event Action<EWearingSlot> OnSlotSelected;
		public event Action<IWearing> OnWearingSelected; 
		
		[SerializeField] private Transform elementsRoot;
		[SerializeField] private WearingSelectElement element;
		[SerializeField] private List<WearingSlotSelectElement> slotSelections;

		private List<WearingSelectElement> _elements = new();
		
		private List<IWearing> _wearings;

		public EWearingSlotMask AvailableSlots
		{
			set
			{
				for (var i = 0; i < slotSelections.Count; i++)
				{
					if ((value & (EWearingSlotMask)(1 << (int)slotSelections[i].Slot)) <= 0)
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

		public List<IWearing> Wearings
		{
			get => _wearings;
			set
			{
				_wearings = value;
				UpdateContent();
			}
		}

		private void Awake()
		{
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

		private void UpdateContent()
		{
			for (var i = 0; i < _wearings.Count; i++)
			{
				var wearingElement = GetElement(i);
				wearingElement.Wearing = _wearings[i];
				wearingElement.Show();
			}

			for (var i = _wearings.Count; i < _elements.Count; i++)
			{
				_elements[i].Hide();
			}
		}

		private WearingSelectElement GetElement(int i)
		{
			if (i < _elements.Count)
			{
				return _elements[i];
			}

			var newElement = Instantiate(element, elementsRoot);
			_elements.Add(newElement);
			newElement.OnWearingSelect += WearingSelected;
			return newElement;
		}

		private void WearingSelected(WearingSelectElement wearing)
		{
			OnWearingSelected?.Invoke(wearing.Wearing);
		}

		public void UpdateSelection(IWearing wearing)
		{
			for (var i = 0; i < _elements.Count; i++)
			{
				_elements[i].Selected = _elements[i].Wearing == wearing;
			}
		}

		private void SlotSelected(EWearingSlot slot)
		{
			OnSlotSelected?.Invoke(slot);
		}
	}
}