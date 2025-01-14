using System;
using System.Collections.Generic;
using UI.Utils;
using Unit.Config;
using Unit.Creation;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Creation
{
	public class RaidCreationPanelView : UIView
	{
		public event Action OnClose;
		public event Action OnApply;
		
		public event Action<UnitConfig> OnArchetypeSelect;

		[SerializeField] private RaidTemplateView raidTemplate;
		[SerializeField] private Transform raidRoot;
		
		[SerializeField] private UnitArchetypeSelectElement element;
		[SerializeField] private Transform archetypesRoot;

		[SerializeField] private Button applyButton;
		[SerializeField] private Button closeButton;

		private SelectableElementsList<UnitConfig> _archetypesList;

		public List<UnitConfig> Archetypes
		{
			set => _archetypesList.Data = value;
		}

		private RaidTemplateView _templateView;
		
		private RaidTemplate _template;
		
		public RaidTemplate Template
		{
			get => _template;
			set
			{
				_template = value;

				CreateVisualisation();
			}
		}

		private void Awake()
		{
			applyButton.onClick.AddListener(ApplyClick);
			closeButton.onClick.AddListener(CloseClick);
		}

		public void Init()
		{
			_archetypesList = new SelectableElementsList<UnitConfig>(element, archetypesRoot);
			_archetypesList.OnSelect += Select;
		}

		private void CreateVisualisation()
		{
			if (_templateView != null)
			{
				Destroy(_templateView.gameObject);
			}

			_templateView = Instantiate(raidTemplate, raidRoot);
			_templateView.Init(Template);
		}

		private void Select(SelectableElement<UnitConfig> element)
		{
			OnArchetypeSelect?.Invoke(element.Data);
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