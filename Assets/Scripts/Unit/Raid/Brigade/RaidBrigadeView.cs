using Game;
using UnityEngine;
using Utils;

namespace Unit.Raid.Brigade
{
	public class RaidBrigadeView : MonoBehaviour, ISelectable
	{
		private RaidView _view;
		
		private RaidBrigade _model;
		
		public void Init(RaidBrigade model)
		{
			_model = model;
			transform.position = _model.Position.F().V3();
		}
		
		public void Select(bool primary)
		{
			_model.Select(primary);
		}
	}
}