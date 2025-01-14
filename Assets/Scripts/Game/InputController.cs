using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
	public class InputController : MonoBehaviour
	{
		[SerializeField] private Camera camera; 
		[SerializeField] private LayerMask layer;
		
		private void Update()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}
			
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				SelectOnMouseClick(true);
				return;
			}
			
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				SelectOnMouseClick(false);
				return;
			}
		}

		private void SelectOnMouseClick(bool primary)
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layer))
			{
				if (hit.collider.TryGetComponent<ISelectable>(out var selectable))
				{
					selectable.Select(primary);
				}
			}
		}
	}
}