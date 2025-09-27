using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public static List<Tile> FindPath(GridManager grid, Tile start, Tile goal, bool attackPath = false)
    {
        var visited = new HashSet<Tile>();
        var queue = new Queue<Tile>();
        var cameFrom = new Dictionary<Tile, Tile>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == goal) break;

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                bool tileStatement = attackPath ? neighbor.Type == TileType.Traversable || neighbor.Type == TileType.Cover : neighbor.Type == TileType.Traversable;
                if (!visited.Contains(neighbor) && tileStatement)
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        if (!cameFrom.ContainsKey(goal)) return null;

        var path = new List<Tile>();
        var t = goal;
        while (t != start)
        {
            path.Add(t);
            t = cameFrom[t];
        }
        path.Reverse();
        return path;
    }

    static IEnumerable<Tile> GetNeighbors(GridManager grid, Tile tile)
    {
        int[,] dirs = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
        for (int i = 0; i < 4; i++)
        {
            int nx = tile.X + dirs[i, 0];
            int ny = tile.Y + dirs[i, 1];
            if (nx >= 0 && ny >= 0 && nx < grid.Width && ny < grid.Height)
                yield return grid.GetTile(nx, ny);
        }
    }
}
