using System.Collections.Generic;
using TMPro;
using UnityEngine;
using World;

namespace UI.Storage
{
	public class StoragePanelView : MonoBehaviour
	{
		[SerializeField] private TMP_Text gold;
		[SerializeField] private TMP_Text goods;
		[SerializeField] private TMP_Text food;
		[SerializeField] private TMP_Text recruits;

		private Dictionary<EResource, TMP_Text> _resourceAmounts;

		private StoragePanel _model;

		private void Awake()
		{
			_resourceAmounts = new Dictionary<EResource, TMP_Text>()
			{
				{ EResource.Gold, gold },
				{ EResource.Goods, goods },
				{ EResource.Food, food },
				{ EResource.Recruits, recruits }
			};
		}

		public void Init(StoragePanel model)
		{
			_model = model;
		}

		public void SetResource(EResource resource, int value)
		{
			_resourceAmounts[resource].text = value.ToString();
		}
	}
}