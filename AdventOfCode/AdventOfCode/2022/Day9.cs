using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day9 : Day
{
    public Day9(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var dict = new Dictionary<string, int>();
        var head = new Position { X = 0, Y = 0 };
        var tail = new Position { X = 0, Y = 0 };
        dict.Add($"{tail.X},{tail.Y}", 1);
        foreach (var line in Input)
        {
            var split = line.Split(" ");
            var count = int.Parse(split[1]);

            while (count > 0)
            {
                MoveHead(head, split[0]);
                if (!AreTouching(head, tail))
                {
                    MoveTail(head, tail);
                    if (!dict.ContainsKey($"{tail.X},{tail.Y}"))
                        dict.Add($"{tail.X},{tail.Y}", 0);
                    dict[$"{tail.X},{tail.Y}"]++;
                }

                count--;
            }
        }

        return dict.Count(x => x.Value > 0);
    }

    protected override decimal SolvePartB()
    {
        var set = new HashSet<(int, int)>();
        var knots = new List<Position>();

        for (int i = 0; i < 10; i++)
        {
            knots.Add(new Position { X = 0, Y = 0, Character = i != 0 ? i.ToString() : "H" });
        }

        set.Add((0, 0));

        foreach (var line in Input)
        {
            var split = line.Split(" ");
            var count = int.Parse(split[1]);

            while (count > 0)
            {
                MoveHead(knots[0], split[0]);

                for (int i = 1; i < 10; i++)
                {
                    if (!AreTouching(knots[i - 1], knots[i]))
                    {
                        MoveTail(knots[i - 1], knots[i]);
                    }
                }

                set.Add((knots[9].X, knots[9].Y));
                count--;
            }
        }

        return set.Count;
    }

    private bool AreTouching(Position head, Position tail) =>
        tail.X >= head.X - 1 && tail.X <= head.X + 1 && tail.Y >= head.Y - 1 && tail.Y <= head.Y + 1;

    private void MoveHead(Position head, string command)
    {
        switch (command)
        {
            case "R":
                head.Y++;
                break;
            case "L":
                head.Y--;
                break;
            case "D":
                head.X++;
                break;
            case "U":
                head.X--;
                break;
        }
    }

    private void MoveTail(Position head, Position tail)
    {
        if (head.X == tail.X)
        {
            tail.Y = head.Y > tail.Y ? tail.Y + 1 : tail.Y - 1;
            return;
        }

        if (head.Y == tail.Y)
        {
            tail.X = head.X > tail.X ? tail.X + 1 : tail.X - 1;
            return;
        }

        if (head.X > tail.X)
        {
            tail.X++;
        }
        else
        {
            tail.X--;
        }

        if (head.Y > tail.Y)
        {
            tail.Y++;
        }
        else
        {
            tail.Y--;
        }
    }

    public class Position
    {
        public int X;
        public int Y;
        public string Character = "H";
    }
}