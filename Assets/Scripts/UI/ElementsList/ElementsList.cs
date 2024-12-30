using System.Collections.Generic;
using UnityEngine;

namespace UI.ElementsList
{
	public class ElementsList<T, TArg> : UIView where T :  UIView, IListElement<TArg>
	{
		[SerializeField] private T elementPrefab;
		[SerializeField] private Transform root;

		private List<T> _elements = new();

		public void RenderList(List<TArg> args)
		{
			if (args.Count == 0)
			{
				HideElements();
				return;
			}
			
			for (var i = 0; i < args.Count; i++)
			{
				RenderElement(i, args[i]);
			}

			HideElements(args.Count);
		}

		private void HideElements(int from = 0)
		{
			for (var i = from; i < _elements.Count; i++)
			{
				_elements[i].Hide();
			}
		}

		private void RenderElement(int i, TArg data)
		{
			T element;
			if (_elements.Count <= i)
			{
				element = CreateElement();
				_elements.Add(element);
			}
			else
			{
				element = _elements[i];
			}

			element.Data = data;
			element.Show();
		}

		private T CreateElement()
		{
			return Instantiate(elementPrefab, root);
		}
	}
}