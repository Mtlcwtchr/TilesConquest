using System;
using System.Collections.Generic;
using System.Linq;
using Unit.Wearing;
using UnityEngine;

namespace Unit.Visual
{
	public class UnitVisualisation : MonoBehaviour
	{
		[Serializable]
		private class WearingSlot
		{
			public EWearingSlot slot;
			public Transform root;
			public GameObject visual;
		}

		[SerializeField] private Animator animator;
		[SerializeField] private WearingSlot[] wearings;
		
		private Dictionary<EWearingSlot, WearingSlot> Wearings { get; set; }

		public Animator Animator => animator;

		public void Awake()
		{
			Wearings = wearings.ToDictionary(wearing => (wearing.slot));
		}

		public void AddWearing(IWearing wearing)
		{
			RemoveWearing(wearing.Slot);

			var slot = Wearings[wearing.Slot];
			var visual = Instantiate(wearing.Config.visual, slot.root);
			slot.visual = visual;
		}

		public void RemoveWearing(EWearingSlot slot)
		{
			var holder = Wearings[slot];
			if (holder.visual == null)
				return;
			
			Destroy(holder.visual);
			holder.visual = null;
		}
	}
}