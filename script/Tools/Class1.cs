using Godot;
using System.Collections.Generic;

public partial class Pathfinder : Node
{
	// Call this to get the shortest path between two grid positions
	// Returns a list of Vector2I positions from source to target (inclusive)
	// Returns empty list if no path found
	public List<Vector2I> FindShortestPath(Vector2I sourcePosition, Vector2I targetPosition)
	{
		// A* open set: (fCost, position)
		var openSet = new SortedSet<(int fCost, int tieBreak, Vector2I pos)>(
			Comparer<(int, int, Vector2I)>.Create((a, b) =>
			{
				int cmp = a.Item1.CompareTo(b.Item1);
				return cmp != 0 ? cmp : a.Item2.CompareTo(b.Item2);
			})
		);

		var gCost = new Dictionary<Vector2I, int>();       // cost from source
		var cameFrom = new Dictionary<Vector2I, Vector2I>(); // path tracking
		int tieBreaker = 0;

		gCost[sourcePosition] = 0;
		int hStart = Heuristic(sourcePosition, targetPosition);
		openSet.Add((hStart, tieBreaker++, sourcePosition));

		// 4-directional movement (up, down, left, right)
		Vector2I[] directions = {
			new Vector2I(1, 0),
			new Vector2I(-1, 0),
			new Vector2I(0, 1),
			new Vector2I(0, -1)
		};

		while (openSet.Count > 0)
		{
			// Pop node with lowest fCost
			var current = openSet.Min;
			openSet.Remove(current);
			Vector2I currentPos = current.pos;

			// Reached target — reconstruct path
			if (currentPos == targetPosition)
				return ReconstructPath(cameFrom, currentPos);

			foreach (var dir in directions)
			{
				Vector2I neighbor = currentPos + dir;

				// Optional: add IsWalkable(neighbor) check here
				// if (!IsWalkable(neighbor)) continue;

				int newGCost = gCost[currentPos] + 1; // each step costs 1

				if (!gCost.ContainsKey(neighbor) || newGCost < gCost[neighbor])
				{
					gCost[neighbor] = newGCost;
					cameFrom[neighbor] = currentPos;
					int fCost = newGCost + Heuristic(neighbor, targetPosition);
					openSet.Add((fCost, tieBreaker++, neighbor));
				}
			}
		}

		return new List<Vector2I>(); // no path found
	}

	// Manhattan distance heuristic — perfect for 4-directional grid movement
	private int Heuristic(Vector2I a, Vector2I b)
	{
		return Mathf.Abs(a.X - b.X) + Mathf.Abs(a.Y - b.Y);
	}

	// Walks back through cameFrom to build the path list
	private List<Vector2I> ReconstructPath(Dictionary<Vector2I, Vector2I> cameFrom, Vector2I current)
	{
		var path = new List<Vector2I>();
		while (cameFrom.ContainsKey(current))
		{
			path.Add(current);
			current = cameFrom[current];
		}
		path.Add(current); // add source
		path.Reverse();
		return path;
	}
}
