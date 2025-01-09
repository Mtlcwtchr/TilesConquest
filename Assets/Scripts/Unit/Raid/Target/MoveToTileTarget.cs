using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Unit.Raid.Target
{
	public class MoveToTileTarget : Target
	{
		private Vector2Int _moveToPosition;

		private World.World _world;

		private List<Vector2Int> _path;
		private int _currentPathIndex;

		public MoveToTileTarget(Raid raid, World.World world, Vector2Int position) : base(raid)
		{
			_world = world;
			_moveToPosition = position;
		}
		
		public override void Start()
		{
			CalculatePath();
		}

		public override void Advance()
		{
			var prevIndex = _currentPathIndex;
			var nextIndex = GetNextIndex();
			if (nextIndex == -1)
			{
				Cancel();
				return;
			}

			_raid.Move(_path, prevIndex, nextIndex);
		}

		private int GetNextIndex()
		{
			if (_currentPathIndex >= _path.Count - 1)
				return -1;
			
			_currentPathIndex += _raid.Speed;
			_currentPathIndex = Mathf.Min(_currentPathIndex, _path.Count - 1);

			return _currentPathIndex;
		}

		private void CalculatePath()
		{
			var availablePositions = _world.TilesManager.GetActiveTiles().Select(tile => tile.Position).ToHashSet();

			var pf = new Pathfinding();
			var path = pf.FindPath(_raid.Position, _moveToPosition, availablePositions);
			_path = path;
			_currentPathIndex = 0;
			
			for (var i = 0; i < path.Count - 1; i++)
			{
				Debug.DrawLine(path[i].F().V3(0.3f), path[i + 1].F().V3(0.3f), Color.red, 360);
			}
		}
	}
}