using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public static class ListUtils
	{
		public static T GetRandom<T>(this List<T> src)
		{
			if (src.Count == 0)
				return default(T);

			return src[Random.Range(0, src.Count)];
		}
	}
}