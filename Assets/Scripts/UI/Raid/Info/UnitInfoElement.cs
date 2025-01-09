using Unit.Visual;
using UnityEngine;

namespace UI.Raid.Info
{
	public class UnitInfoElement : UIView
	{
		[SerializeField] private Transform root;

		private UnitVisualisation _currentVisual;
		
		private Unit.Unit _unit;
		public Unit.Unit Unit
		{
			get => _unit;
			set
			{
				if (_unit == value)
					return;

				_unit = value;
				if (_currentVisual != null)
				{
					Destroy(_currentVisual.gameObject);
				}

				if (_unit == null)
					return;
				
				_currentVisual = Instantiate(_unit.View.Visualisation, root);
			}
		}
	}
}