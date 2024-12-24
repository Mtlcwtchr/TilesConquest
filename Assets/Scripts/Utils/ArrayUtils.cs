using System;

namespace Utils
{
	public static class ArrayUtils
	{
		public static int CheckIndex<T>(this T[] arr, int index)
		{
			return Math.Clamp(index, 0, arr.Length - 1);
		}
	}
}