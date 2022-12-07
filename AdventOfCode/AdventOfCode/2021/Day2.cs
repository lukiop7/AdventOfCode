using AdventOfCode.Model;

namespace AdventOfCode._2021;

public class Day2 : Day
{
    public Day2(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var ship = new Ship();
        foreach (var line in Input)
        {
            ship.Execute(line);
        }

        return ship.Position * ship.Depth;
    }


    protected override decimal SolvePartB()
    {
        var ship = new Ship();
        foreach (var line in Input)
        {
            ship.ExecuteWithAim(line);
        }

        return ship.Position * ship.Depth;
    }

    public class Ship
    {
        public int Depth = 0;
        public int Aim = 0;
        public int Position = 0;

        public void Execute(string command)
        {
            var split = command.Split(" ");
            switch (split[0])
            {
                case "forward":
                    Position += int.Parse(split[1]);
                    break;
                case "up":
                    Depth -= int.Parse(split[1]);
                    break;
                case "down":
                    Depth += int.Parse(split[1]);
                    break;
            }
        }

        public void ExecuteWithAim(string command)
        {
            var split = command.Split(" ");
            switch (split[0])
            {
                case "forward":
                    Position += int.Parse(split[1]);
                    Depth += int.Parse(split[1]) * Aim;
                    break;
                case "up":
                    Aim -= int.Parse(split[1]);
                    break;
                case "down":
                    Aim += int.Parse(split[1]);
                    break;
            }
        }
    }
}