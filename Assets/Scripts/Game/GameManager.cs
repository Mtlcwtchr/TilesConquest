using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game
{
	public class GameManager
	{
		public event Action OnTurnFinish;
		
		private List<Player> _players;

		private World.World _world;
		private Queue<Player> _playersQueue;
		
		public Player Player { get; set; }

		public static GameManager Instance { get; private set; }

		public GameManager(List<Player> players, World.World world)
		{
			Instance = this;
			_players = players;
			_playersQueue = new();
			for (var i = 0; i < _players.Count; i++)
			{
				_players[i].OnTurnFinish += TurnFinish;
			}
			_world = world;
		}

		public void Update()
		{
			_world.Update();
			
			_playersQueue.Clear();
			for (var i = 0; i < _players.Count; i++)
			{
				_playersQueue.Enqueue(_players[i]);
			}

			NotifyNextPlayer();
		}

		private async void NotifyNextPlayer()
		{
			if (_playersQueue.Count < 1)
			{
				await Task.Delay(100);
				OnTurnFinish?.Invoke();
				return;
			}
			
			var player = _playersQueue.Dequeue();
			player.NotifyTurn();
		}

		private void TurnFinish()
		{
			NotifyNextPlayer();
		}
	}
}