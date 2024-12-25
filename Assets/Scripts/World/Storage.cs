using System;
using System.Collections.Generic;

namespace World
{
	public class Storage
	{
		public event Action<EResource, int> OnResourceAdvanced;
		
		private Dictionary<EResource, int> _resources;

		public Storage(params EResource[] resources)
		{
			_resources = new Dictionary<EResource, int>(resources.Length);
			for (var i = 0; i < resources.Length; i++)
			{
				_resources.Add(resources[i], 0);
			}
		}

		public int Get(EResource resource)
		{
			return _resources[resource];
		}

		public void Advance(EResource resource, int value)
		{
			var newVal = Math.Max(_resources[resource] + value, 0);
			_resources[resource] = newVal;
			
			OnResourceAdvanced?.Invoke(resource, newVal);
		}
	}
}