﻿using System;
using System.Collections.Generic;
using Unit.Config;
using Unit.Visual;
using Unit.Wearing;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unit.Creation
{
	public class UnitTemplate
	{
		public event Action<IWearing> OnWearingEquip;
		public event Action<EWearingSlot> OnWearingUnEquip; 
		
		public UnitConfig Config { get; }
		public int Hp { get; set; }
		public int Damage { get; set; }
		public int Speed { get; set; }
		public int Priority { get; set; }
		
		public UnitVisualisation View { get; set; }
		
		public Dictionary<EWearingSlot, IWearing> Wearings { get; }

		public UnitTemplate(UnitConfig config)
		{
			Config = config;
			
			Hp = config.hp;
			Damage = config.damage;
			Speed = config.speed;
			Priority = config.priority;

			Wearings = new();
		}

		public bool IsWearingAvailable(IWearing wearing)
		{
			return (Config.availableWearings & (EWearingSlotMask)(1 << (int)wearing.Slot)) <= 0;
		}

		public bool TryEquip(IWearing wearing)
		{
			if ((Config.availableWearings & (EWearingSlotMask)(1 << (int)wearing.Slot)) <= 0)
			{
				return false;
			}

			Equip(wearing);
			return true;
		}

		private void Equip(IWearing wearing)
		{
			UnEquipCurrent(wearing.Slot);
			wearing.Equip(this);
			Wearings[wearing.Slot] = wearing;
			OnWearingEquip?.Invoke(wearing);
		}

		public void UnEquipCurrent(EWearingSlot slot)
		{
			if (!Wearings.TryGetValue(slot, out var currentlyEquipped) || currentlyEquipped == null) return;
			
			currentlyEquipped.UnEquip(this);
			Wearings[slot] = null;
			OnWearingUnEquip?.Invoke(slot);
		}

		public bool Equipped(IWearing wearing)
		{
			return Wearings.TryGetValue(wearing.Slot, out var currentlyEquipped) && currentlyEquipped == wearing;
		}

		public IWearing GetEquipped(EWearingSlot slot)
		{
			return Wearings.GetValueOrDefault(slot);
		}

		public UnitVisualisation CreateVisualisation(Transform root, int layer = 0)
		{
			var visualisation = Object.Instantiate(Config.visualBase, root);
			visualisation.Layer = layer;
			foreach (var (_, wearing) in Wearings)
			{
				visualisation.AddWearing(wearing);
			}

			return visualisation;
		}
	}
}