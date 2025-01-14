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

		private int _layer;

		public int Layer
		{
			get => _layer;
			set
			{
				_layer = value;
				
				var children = GetComponentsInChildren<Transform>(true);
				for (var i = 0; i < children.Length; i++)
				{
					children[i].gameObject.layer = _layer;
				}
			}
		}
		
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
			visual.layer = Layer;
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

		public void SetPlaceholder(Transform root)
		{
			transform.SetParent(root, false);
		}
	}
}