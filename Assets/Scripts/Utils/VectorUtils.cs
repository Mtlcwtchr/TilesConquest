using UnityEngine;

namespace Utils
{
	public static class VectorUtils
	{
		public static Vector2 V2(this Vector3 src)
		{
			return new Vector2(src.x, src.z);
		}

		public static Vector3 V3(this Vector2 src, float y = 0)
		{
			return new Vector3(src.x, y, src.y);
		}

		public static void Clamp(this Vector2Int src)
		{
			src.Clamp(new(-1, -1), new(1, 1));
		}

		public static Vector2Int I(this Vector2 src)
		{
			return new Vector2Int((int)src.x, (int)src.y);
		}

		public static Vector2 F(this Vector2Int src)
		{
			return src;
		}
	}
}