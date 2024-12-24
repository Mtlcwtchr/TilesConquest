using UnityEngine;
using Utils;

namespace Controls
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera camera;
		[SerializeField] private float speed;
		
		private const float _mouseMoveDelta = 10 * 10;
		
		private bool _mousePressed;
		private Vector2 _mousePosition;
		private Vector2 _pressPosition;

		private Vector3 _moveDelta;
		
		private void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				_mousePressed = true;
				_pressPosition = Input.mousePosition;
				_mousePosition = _pressPosition;
			}

			if (Input.GetMouseButtonUp(1))
			{
				_mousePressed = false;
				_moveDelta = Vector3.zero;
			}

			if (!_mousePressed)
				return;

			_mousePosition = Input.mousePosition;
			_moveDelta = (_pressPosition - _mousePosition).V3();
			_moveDelta.Normalize();
		}

		private void FixedUpdate()
		{
			if (_moveDelta == Vector3.zero)
				return;
			
			var newPosition = camera.transform.position + _moveDelta * (Time.deltaTime * speed);
			camera.transform.position = newPosition;
		}
	}
}