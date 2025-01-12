namespace UI
{
	public class UIModel<T> where T : UIView
	{
		protected T _view;
		
		public virtual bool Locked { get; set; }

		public UIModel(T view)
		{
			_view = view;
		}

		public virtual void Show()
		{
			_view.Show();
		}

		public virtual void Hide()
		{
			_view.Hide();
		}
	}
}