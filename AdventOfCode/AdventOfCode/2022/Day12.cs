using AdventOfCode.Model;
using System.Numerics;

namespace AdventOfCode._2022;

public class Day12 : Day
{
    public Day12(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var array = Input.Select(x => x.Select(y => y).ToArray()).ToArray();

        var start = IndexOf(array, 'S');

        return BFS(array, start.FirstOrDefault());
    }

    protected override decimal SolvePartB()
    {
        var array = Input.Select(x => x.Select(y => y).ToArray()).ToArray();

        var start = IndexOf(array, 'a');

        var distances = new List<int>();

        foreach (var candidate in start)
        {
            distances.Add(BFS(array, candidate));
        }

        return distances.Min();
    }

    private int BFS(char[][] array, (int y, int x) startIndex)
    {
        var visited = new HashSet<(int y, int x)>();

        var queue = new Queue<(int y, int x, int count)>();
        queue.Enqueue((startIndex.y, startIndex.x, -1));
        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();

            vertex.count++;

            if (array[vertex.y][vertex.x] == 'E')
                return vertex.count;

            if (visited.Contains((vertex.y, vertex.x)))
                continue;

            visited.Add((vertex.y, vertex.x));

            foreach (var neighbor in GetNeighbours((vertex.y, vertex.x), array))
                if (!visited.Contains(neighbor))
                    queue.Enqueue((neighbor.y, neighbor.x, vertex.count));
        }

        return int.MaxValue;
    }

    private IList<(int y, int x)> GetNeighbours((int y, int x) indices, char[][] array)
    {
        var neighbours = new List<(int y, int x)>();

        var current = array[indices.y][indices.x];

        if (indices.x != 0 && IsAvailable(current, array[indices.y][indices.x - 1]))
            neighbours.Add((indices.y, indices.x - 1));
        if (indices.x != array[0].Length - 1 && IsAvailable(current, array[indices.y][indices.x + 1]))
            neighbours.Add((indices.y, indices.x + 1));
        if (indices.y != 0 && IsAvailable(current, array[indices.y - 1][indices.x]))
            neighbours.Add((indices.y - 1, indices.x));
        if (indices.y != array.Length - 1 && IsAvailable(current, array[indices.y + 1][indices.x]))
            neighbours.Add((indices.y + 1, indices.x));

        return neighbours;
    }

    private bool IsAvailable(char current, char candidate)
    {
        if (current == 'S') current = 'a';
        if (candidate == 'E') candidate = 'z';
        return candidate - current <= 1;
    }

    private IList<(int y, int x)> IndexOf(char[][] jaggedArray, char value)
    {
        var list = new List<(int y, int x)>();
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            char[] array = jaggedArray[i];
            for (int j = 0; j < array.Length; j++)
                if (array[j].Equals(value) || array[j].Equals('S'))
                    list.Add((i, j));
        }

        return list;
    }
}