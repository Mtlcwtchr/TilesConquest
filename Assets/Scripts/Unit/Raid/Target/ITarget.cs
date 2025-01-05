namespace Unit.Raid.Target
{
	public interface ITarget
	{
		public void Start();
		public void Advance();
		public void Cancel();
	}
}