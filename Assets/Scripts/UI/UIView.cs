using UnityEngine;

namespace UI
{
	public abstract class UIView : MonoBehaviour
	{
		private GameObject _gameObject;
		private bool? _active;

		protected GameObject GameObject => _gameObject ??= gameObject;
		protected bool Active => _active ??= GameObject.activeSelf;

		public void Show()
		{
			SetActive(true);
		}

		public void Hide()
		{
			SetActive(false);
		}

		private void SetActive(bool active)
		{
			if (_active == active)
				return;

			_active = active;
			GameObject.SetActive(Active);
		}
	}
}