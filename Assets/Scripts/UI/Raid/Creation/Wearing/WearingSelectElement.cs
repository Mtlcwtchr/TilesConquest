using System;
using Unit.Wearing;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation.Wearing
{
	public class WearingSelectElement : UIView
	{
		public event Action<WearingSelectElement> OnWearingSelect; 
		
		[SerializeField] private Image icon;
		[SerializeField] private Button button;
		[SerializeField] private GameObject selection;

		private IWearing _wearing;
		public IWearing Wearing
		{
			get => _wearing;
			set
			{
				_wearing = value;
				icon.sprite = _wearing.Config.icon;
			}
		}

		private bool _selected;
		public bool Selected
		{
			get => _selected;
			set
			{
				_selected = value;
				selection.SetActive(_selected);
			}
		}

		private void Awake()
		{
			button.onClick.AddListener(ButtonClick);
		}

		private void ButtonClick()
		{
			OnWearingSelect?.Invoke(this);
		}
	}
}