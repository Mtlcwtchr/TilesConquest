using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Utils
{
	public abstract class SelectableElement<T> : UIView
	{
		public event Action<SelectableElement<T>> OnSelect;
		
		[SerializeField] private Button button;

		private T _data;

		public T Data
		{
			get => _data;
			set
			{
				_data = value;
				UpdateContent();
			}
		}

		public bool Interactable
		{
			set => button.interactable = value;
		}

		public abstract bool Selected { get; set; }
		
		public SelectableElement<T> CreateInstance(Transform root)
		{
			return Instantiate(this, root);
		}

		private void Awake()
		{
			button.onClick.AddListener(ButtonClick);
		}

		protected abstract void UpdateContent();

		private void ButtonClick()
		{
			OnSelect?.Invoke(this);
		}
	}
}