namespace Unit.Raid.Target
{
	public class ChaseRaidTarget : Target
	{
		private Raid _target;
		
		public ChaseRaidTarget(Raid attacker, Raid target) : base(attacker)
		{
			_target = target;
		}
		
		public override void Start()
		{
			
		}

		public override void Advance()
		{
			
		}
	}
}