using System;

namespace Unit.Wearing
{
	public enum EWearingSlot
	{
		Chest = 0,
		HandR = 1,
		HandL = 2,
		LegR = 3,
		LegL = 4,
		Head = 5,
		Max = 6,
	}

	[Flags]
	public enum EWearingSlotMask
	{
		Chest = 1 << EWearingSlot.Chest,
		HandR = 1 << EWearingSlot.HandR,
		HandL = 1 << EWearingSlot.HandL,
		LegR = 1 << EWearingSlot.LegR,
		LegL = 1 << EWearingSlot.LegL,
		Head = 1 << EWearingSlot.Head,
	}
}