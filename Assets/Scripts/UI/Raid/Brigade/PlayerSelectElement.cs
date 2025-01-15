using Game;
using UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Raid.Brigade
{
	public class PlayerSelectElement : SelectableElement<Player>
	{
		[SerializeField] private Image icon;
		
		public override bool Selected { get; set; }
		
		protected override void UpdateContent()
		{
			
		}
	}
}