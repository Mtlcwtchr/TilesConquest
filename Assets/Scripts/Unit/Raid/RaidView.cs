using System.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Unit.Raid
{
	public class RaidView : MonoBehaviour
	{
		private Vector2 _currentPosition;
		private Raid _model;

		private RaidManager _manager;
		
		public void Init(Raid model, RaidManager manager)
		{
			_manager = manager;
			_model = model;
			_model.Position = transform.position.V2().I();
			_currentPosition = _model.Position;
			_model.OnPositionUpdate += UpdatePosition;
		}

		private void UpdatePosition(Vector2Int newPosition)
		{
			UpdatePositionOnTime(newPosition);
		}

		private async void UpdatePositionOnTime(Vector2Int desiredPosition)
		{
			var t = 0f;
			var delay = 10;
			var tChange = delay / 1000f;
			while (!_currentPosition.Equals(desiredPosition))
			{
				_currentPosition = Vector2.Lerp(_currentPosition, desiredPosition, t);
				transform.position = _currentPosition.V3();
				t += tChange;
				await Task.Delay(delay);
			}
		}

		private void OnMouseDown()
		{
			_manager.MoveToRandom(_model);
		}
	}
}