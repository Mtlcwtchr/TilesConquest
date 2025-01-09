using System;

namespace Unit.Raid.Target
{
	public abstract class Target : ITarget
	{
		public event Action OnCancelled;

		protected Raid _raid;

		protected Target(Raid raid)
		{
			_raid = raid;
		}
		
		public abstract void Start();

		public abstract void Advance();

		public virtual void Cancel()
		{
			OnCancelled?.Invoke();
		}
	}
}