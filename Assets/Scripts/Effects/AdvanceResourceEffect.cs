using System;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace Effects
{
	[CreateAssetMenu(fileName = "AdvanceResourceEffect", menuName = "Tiles/Effects/ResourceAdvance", order = 0)]
	public class AdvanceResourceEffect : TileEffect
	{
		[Serializable]
		private struct ResourceAdvance
		{
			public EResource resource;
			public int value;
		}

		[SerializeField] private List<ResourceAdvance> resources;
		
		public override void Apply(World.World world)
		{
			var storage = world.Storage;
			for (var i = 0; i < resources.Count; i++)
			{
				var resource = resources[i];
				storage.Advance(resource.resource, resource.value);
			}
		}
	}
}