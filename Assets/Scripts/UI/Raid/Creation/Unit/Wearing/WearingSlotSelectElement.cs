using System;
using Unit.Wearing;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation.Unit.Wearing
{
	public class WearingSlotSelectElement : UIView
	{
		public event Action<EWearingSlot> OnSlotSelected; 
		
		[SerializeField] private Button button;
		[SerializeField] private EWearingSlot slot;

		public EWearingSlot Slot => slot;

		public bool Selected
		{
			set
			{
				button.interactable = !value;
			}
		}

		private void Awake()
		{
			button.onClick.AddListener(ButtonClick);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveListener(ButtonClick);
		}

		private void ButtonClick()
		{
			OnSlotSelected?.Invoke(Slot);
		}
	}
}