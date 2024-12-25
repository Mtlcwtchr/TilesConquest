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

		public void Show()
		{
			_view.Show();
		}

		public void Hide()
		{
			_view.Hide();
		}
	}
}