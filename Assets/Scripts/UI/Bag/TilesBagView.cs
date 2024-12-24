using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bag
{
	public class TilesBagView : MonoBehaviour
	{
		public event Action OnButtonClick;
		
		[SerializeField] private Button button;

		private TilesBag _model;
		
		public bool Enabled
		{
			get => button.interactable;
			set => button.interactable = value;
		}

		public void Init(TilesBag model)
		{
			_model = model;
			_model.BindView(this);
		}
		
		private void Awake()
		{
			button.onClick.AddListener(ButtonClick);
		}

		private void OnDestroy()
		{
			button.onClick.RemoveListener(ButtonClick);
		}

		private void ButtonClick()
		{
			OnButtonClick?.Invoke();
		}
	}
}