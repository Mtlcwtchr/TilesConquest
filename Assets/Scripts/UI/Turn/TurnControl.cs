using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Turn
{
	public class TurnControl : MonoBehaviour
	{
		[SerializeField] private GameObject finish;
		[SerializeField] private GameObject wait;
		[SerializeField] private Button button;

		private Player _player;

		private void Awake()
		{
			button.onClick.AddListener(Click);
		}

		public void Init(Player player)
		{
			_player = player;
			
			_player.OnTurn += Turn;
			_player.OnTurnFinish += TurnFinish;
		}

		private void Click()
		{
			_player.FinishTurn();
		}

		private void TurnFinish()
		{
			finish.SetActive(false);
			wait.SetActive(true);
		}

		private void Turn(ETurnPhase turn)
		{
			switch (turn)
			{
				case ETurnPhase.PlaceTile:
					finish.SetActive(true);
					button.interactable = false;
					wait.SetActive(false);
					break;
				case ETurnPhase.Decide:
					button.interactable = true;
					break;
			}
		}
	}
}