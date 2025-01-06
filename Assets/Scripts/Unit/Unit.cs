namespace Unit
{
	public class Unit
	{
		private int _maxHp;
		private int _hp;
		private int _damage;
		private int _range;

		private int _priority = 1;

		public float RelativeHP => _hp / (float)_maxHp;

		public int Priority => _priority;
		
		public UnitView View { get; private set; }

		public Unit(UnitView view)
		{
			View = view;
		}
	}
}