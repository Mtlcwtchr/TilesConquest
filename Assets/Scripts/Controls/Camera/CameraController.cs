using System;
using UnityEngine;
using Utils;

namespace Controls
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Camera camera;
		[SerializeField] private float speed;
		
		[SerializeField] private float zoomSpeed = 10f;
		[SerializeField] private float minZoom = 10f;
		[SerializeField] private float maxZoom = 60f;
		
		private float _currentZoom;
		private float _scrollDelta;
		private int _scrollDir;
		
		private bool _moveRequested;
		private Vector2 _mousePosition;
		private Vector2 _pressPosition;
		private Vector3 _moveDelta;

		private void Awake()
		{
			_currentZoom = camera.fieldOfView;
		}

		private void Update()
		{
			CheckMovementByMouse();
			CheckScrollByMouse();
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
			var scroll = Input.mouseScrollDelta.y;
			_scrollDelta += Mathf.Abs(scroll);
			var scrollDir = scroll == 0 ? _scrollDir : Math.Sign(scroll);
			if (scrollDir != _scrollDir)
			{
				_scrollDir = scrollDir;
				_scrollDelta = Mathf.Abs(scroll);
			}
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
			UpdateMovement();
			UpdateScroll();
		}

		private void UpdateScroll()
		{
			if (Mathf.Approximately(_scrollDelta, 0))
				return;

			_scrollDelta--;

			if (Mathf.Approximately(_currentZoom, minZoom) && _scrollDir > 0)
				return;
			
			if (Mathf.Approximately(_currentZoom, maxZoom) && _scrollDir < 0)
				return;
			
			var change = _scrollDir * Time.fixedDeltaTime * zoomSpeed;
			_currentZoom -= change;
			_currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
			camera.fieldOfView = _currentZoom;
			
			var t = ((_currentZoom - minZoom) / (maxZoom - minZoom));
			camera.transform.position -= new Vector3(0, 0, change * 0.1f);
			camera.transform.eulerAngles = new Vector3(Mathf.Lerp(40, 80, t), camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
		}

		private void UpdateMovement()
		{
			
			if (_moveDelta == Vector3.zero)
				return;
			
			var newPosition = camera.transform.position + _moveDelta * (Time.fixedDeltaTime * speed);
			camera.transform.position = newPosition;
		}
	}
}