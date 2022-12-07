using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day4 : Day
{
    public Day4(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        return Input
            .Where(x =>
            {
                var split = x.Split(",");
                var first = new Space(split[0]);
                var second = new Space(split[1]);
                return first.IsContained(second);
            })
            .Count();
    }

    protected override decimal SolvePartB()
    {
        return Input
            .Where(x =>
            {
                var split = x.Split(",");
                var first = new Space(split[0]);
                var second = new Space(split[1]);
                return first.IsOverlapping(second);
            })
            .Count();
    }

    public readonly record struct Space
    {
        public int Start { get; init; }
        public int End { get; init; }

        public Space(string input)
        {
            var split = input.Split("-");
            Start = int.Parse(split[0]);
            End = int.Parse(split[1]);
        }

        public bool IsContained(Space second)
        {
            if ((Start >= second.Start && End <= second.End) || (Start <= second.Start && End >= second.End))
                return true;

            return false;
        }

        public bool IsOverlapping(Space second)
        {
            return Start <= second.End && second.Start <= End;
        }
    }
}