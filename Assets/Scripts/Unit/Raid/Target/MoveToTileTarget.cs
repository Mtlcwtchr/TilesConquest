using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Unit.Raid.Target
{
	public class MoveToTileTarget : ITarget
	{
		public event Action OnCancelled;
		
		private Vector2Int _moveToPosition;
		private Raid _raid;

		private World.World _world;

		private List<Vector2Int> _path;
		private int _currentPathIndex;

		public MoveToTileTarget(Raid raid, World.World world, Vector2Int position)
		{
			_world = world;
			_raid = raid;
			_moveToPosition = position;
		}
		
		public void Start()
		{
			CalculatePath();
		}

		public void Advance()
		{
			var dir = GetNextPos();
			if (dir == Vector2Int.zero)
			{
				Cancel();
				return;
			}

			_raid.Move(dir);
		}

		private Vector2Int GetNextPos()
		{
			if(_currentPathIndex >= _path.Count)
				return Vector2Int.zero;
			
			return _path[_currentPathIndex++];
		}

		private void CalculatePath()
		{
			var availablePositions = _world.TilesManager.GetActiveTiles().Select(tile => tile.Position).ToHashSet();

			var pf = new Pathfinding();
			var path = pf.FindPath(_raid.Position, _moveToPosition, availablePositions);
			_path = path;
			_currentPathIndex = 1;
			
			for (var i = 0; i < path.Count - 1; i++)
			{
				Debug.DrawLine(path[i].F().V3(0.3f), path[i + 1].F().V3(0.3f), Color.red, 360);
			}
		}

		public void Cancel()
		{
			OnCancelled?.Invoke();
		}
	}
}