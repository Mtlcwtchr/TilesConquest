using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Utils
{
	public class SelectableElementsList<T>
	{
		public event Action<SelectableElement<T>> OnSelect;
		
		private Transform _root;
		private SelectableElement<T> _template;

		public List<SelectableElement<T>> Elements { get; }
		
		private List<T> _data;
		public List<T> Data
		{
			get => _data;
			set
			{
				_data = value;
				UpdateContent();
			}
		}

		public SelectableElementsList(SelectableElement<T> template, Transform root)
		{
			_template = template;
			_root = root;

			Elements = new();
		}

		private void UpdateContent()
		{
			for (var i = 0; i < Data.Count; i++)
			{
				var element = GetElement(i);
				element.Data = Data[i];
				element.Show();
			}

			for (var i = Data.Count; i < Elements.Count; i++)
			{
				Elements[i].Hide();
			}
		}

		private SelectableElement<T> GetElement(int i)
		{
			if (i < Elements.Count)
			{
				return Elements[i];
			}

			var newElement = _template.CreateInstance(_root);
			Elements.Add(newElement);
			newElement.OnSelect += Select;
			return newElement;
		}

		private void Select(SelectableElement<T> element)
		{
			OnSelect?.Invoke(element);
		}
	}
}