namespace Unit
{
	public class Unit
	{
		private int _maxHp;
		private int _hp;
		private int _damage;
		private int _range;

		public float RelativeHP => _hp / (float)_maxHp;
	}
}