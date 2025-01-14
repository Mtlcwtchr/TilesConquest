using System;
using Unit.Creation;
using Unit.Visual;
using Unit.Wearing;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation.Unit
{
	public class UnitCreationPanelView : UIView
	{
		public event Action OnClose;
		public event Action OnApply;
		
		[SerializeField] private Transform visualisationRoot;
		[SerializeField] private Button applyButton;
		[SerializeField] private Button closeButton;
		
		private UnitCreationPanel _model;
		
		private UnitVisualisation _visualisation;
		
		private UnitTemplate _template;
		
		public UnitTemplate Template
		{
			get => _template;
			set
			{
				UnsubscribeTemplate();
				
				_template = value;
				SubscribeTemplate();
				
				CreateVisualisation();
			}
		}

		private void Awake()
		{
			applyButton.onClick.AddListener(ApplyClick);
			closeButton.onClick.AddListener(CloseClick);
		}

		private void SubscribeTemplate()
		{
			if (_template == null)
				return;
			
			_template.OnWearingEquip += WearingEquip;
			_template.OnWearingUnEquip += WearingUnEquip;
		}

		private void UnsubscribeTemplate()
		{
			if (_template == null)
				return;
			
			_template.OnWearingEquip -= WearingEquip;
			_template.OnWearingUnEquip -= WearingUnEquip;
		}

		private void WearingEquip(IWearing wearing)
		{
			_visualisation.AddWearing(wearing);
		}

		private void WearingUnEquip(EWearingSlot obj)
		{
			_visualisation.RemoveWearing(obj);
		}

		public void Init(UnitCreationPanel model)
		{
			_model = model;
		}

		private void CreateVisualisation()
		{
			if (_visualisation != null)
			{
				Destroy(_visualisation.gameObject);
			}

			_visualisation = Template.CreateVisualisation(visualisationRoot, LayerMask.NameToLayer("UI"));
			_visualisation.transform.localScale = new(200, 200, 1);
			_visualisation.transform.localRotation = Quaternion.Euler(0, 0, 0);
			_visualisation.transform.localPosition = new(0, 100, 0);
			foreach (var (_, wearing) in Template.Wearings)
			{
				_visualisation.AddWearing(wearing);
			}
		}

		private void ApplyClick()
		{
			OnApply?.Invoke();
		}

		private void CloseClick()
		{
			OnClose?.Invoke();
		}
		
	}
}