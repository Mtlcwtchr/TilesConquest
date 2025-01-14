using System;
using TMPro;
using UI.Raid.Creation;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Info
{
	public class RaidInfoPanelView : UIView
	{
		public event Action OnClose;
		
		[SerializeField] private RaidTemplateView raidTemplate;
		[SerializeField] private Transform raidRoot;
		
		[SerializeField] private TMP_Text hp;
		[SerializeField] private TMP_Text damage;
		[SerializeField] private TMP_Text speed;
		[SerializeField] private Image hpBar;

		[SerializeField] private Button closeButton;

		private RaidInfoPanel _model;

		private RaidTemplateView _templateView;
		
		private Unit.Raid.Raid _raid;

		public Unit.Raid.Raid Raid
		{
			get => _raid;
			set
			{
				_raid = value;
				
				hp.text = $"{_raid.Hp}/{_raid.MaxHp}";
				damage.text = _raid.Damage.ToString();
				speed.text = _raid.Speed.ToString();
				hpBar.fillAmount = _raid.RelativeHp;
				
				CreateVisualisation();
			}
		}

		private void Awake()
		{
			closeButton.onClick.AddListener(CloseClick);
		}

		public void Init(RaidInfoPanel model)
		{
			_model = model;
		}

		private void CreateVisualisation()
		{
			if (_templateView != null)
			{
				Destroy(_templateView.gameObject);
			}

			_templateView = Instantiate(raidTemplate, raidRoot);
			_templateView.Init(Raid.Template);
		}

		private void CloseClick()
		{
			OnClose?.Invoke();
		}
	}
}