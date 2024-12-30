using System;
using System.Collections.Generic;
using Tiles.Model;
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

		public override string Description
		{
			get
			{
				var value = base.Description;
				for (var i = 0; i < resources.Count; i++)
				{
					value = value.Replace($"${resources[i].resource.ToString().ToLower()}", resources[i].value.ToString());
				}
				return value;
			}
		}

		[SerializeField] private List<ResourceAdvance> resources;
		
		public override void Apply(TileFill source, World.World world)
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