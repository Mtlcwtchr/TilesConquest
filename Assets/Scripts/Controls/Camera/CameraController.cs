using UnityEngine;
using Utils;

namespace Controls
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera camera;
		[SerializeField] private float speed;
		[SerializeField] private float scrollSpeed;
		
		private bool _moveRequested;
		private Vector2 _mousePosition;
		private Vector2 _pressPosition;

		private Vector3 _moveDelta;
		
		private void Update()
		{
			CheckMovementByMouse();
			//CheckMovementByTouch();
		}

		private void CheckMovementByMouse()
		{
			if (Input.GetMouseButtonDown(1))
			{
				_moveRequested = true;
				_pressPosition = Input.mousePosition;
				_mousePosition = _pressPosition;
			}

			if (Input.GetMouseButtonUp(1))
			{
				_moveRequested = false;
				_moveDelta = Vector3.zero;
			}

			if (!_moveRequested)
				return;

			_mousePosition = Input.mousePosition;
			_moveDelta = (_pressPosition - _mousePosition).V3();
			_moveDelta.Normalize();
		}

		private void CheckScrollByMouse()
		{
			var mouseScrollDelta = Input.mouseScrollDelta;
		}

		private void CheckMovementByTouch()
		{
			if (Input.touchCount != 1)
			{
				_moveRequested = false;
				_moveDelta = Vector3.zero;
				return;
			}

			var touch = Input.GetTouch(0);
			
			if (!_moveRequested)
			{
				_moveRequested = true;
				_pressPosition = touch.position;
				_mousePosition = _pressPosition;
			}

			if (!_moveRequested)
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