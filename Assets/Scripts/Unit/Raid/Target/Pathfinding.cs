using System.Collections.Generic;
using UnityEngine;

namespace Unit.Raid.Target
{
	public class Pathfinding
	{
		public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> walkableCells)
		{
			// Open and Closed lists
			List<Node> openList = new List<Node>();
			HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();

			// Add start node to Open List
			openList.Add(new Node(start, null, 0, Heuristic(start, goal)));

			while (openList.Count > 0)
			{
				// Get node with lowest F cost
				Node currentNode = openList[0];
				foreach (var node in openList)
					if (node.F < currentNode.F || (node.F == currentNode.F && node.H < currentNode.H))
						currentNode = node;

				// Remove current node from openList and add to closedList
				openList.Remove(currentNode);
				closedList.Add(currentNode.Position);

				// If goal is reached, construct the path
				if (currentNode.Position == goal)
					return ConstructPath(currentNode);

				// Check neighbors
				foreach (var neighborPosition in GetNeighbors(currentNode.Position))
				{
					// Skip if not walkable or already evaluated
					if (!walkableCells.Contains(neighborPosition) || closedList.Contains(neighborPosition))
						continue;

					// Calculate G cost for the neighbor
					var gCost = currentNode.G + 1; // Assuming uniform cost of 1 for moving to a neighbor

					// Check if neighbor is in open list
					Node neighborNode = openList.Find(n => n.Position == neighborPosition);
					if (neighborNode == null)
					{
						// Add new neighbor to open list
						var hCost = Heuristic(neighborPosition, goal);
						openList.Add(new Node(neighborPosition, currentNode, gCost, hCost));
					}
					else if (gCost < neighborNode.G)
					{
						// Update G cost and reassign parent if a better path is found
						neighborNode.G = gCost;
						neighborNode.Parent = currentNode;
					}
				}
			}

			// No path found
			return new List<Vector2Int>();
		}

		private int Heuristic(Vector2Int a, Vector2Int b)
		{
			// Manhattan Distance
			return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
		}

		private List<Vector2Int> GetNeighbors(Vector2Int position)
		{
			// Define possible directions (up, down, left, right)
			List<Vector2Int> directions = new List<Vector2Int>
			{
				new Vector2Int(0, 1),
				new Vector2Int(0, -1),
				new Vector2Int(1, 0),
				new Vector2Int(-1, 0)
			};

			List<Vector2Int> neighbors = new List<Vector2Int>();
			foreach (var dir in directions) neighbors.Add(position + dir);
			return neighbors;
		}

		private List<Vector2Int> ConstructPath(Node node)
		{
			List<Vector2Int> path = new List<Vector2Int>();
			while (node != null)
			{
				path.Add(node.Position);
				node = node.Parent;
			}

			path.Reverse();
			return path;
		}

		public class Node
		{
			public int G; // Cost from start to current node
			public int H; // Heuristic cost to goal
			public Node Parent;
			public Vector2Int Position;

			public Node(Vector2Int position, Node parent, int g, int h)
			{
				Position = position;
				Parent = parent;
				G = g;
				H = h;
			}

			public int F => G + H; // Total cost
		}
	}
}