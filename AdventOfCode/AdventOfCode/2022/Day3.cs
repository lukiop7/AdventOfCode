using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day3 : Day
{
    public Day3(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        return Input
            .Select(x => GetPriority(x.Take(x.Length / 2).Intersect(x.Skip(x.Length / 2)).Single()))
            .Sum();
    }

    protected override decimal SolvePartB()
    {
        return Input.Chunk(3).Select(x => GetPriority(x[0].Intersect(x[1]).Intersect(x[2]).Single()))
            .Sum();
    }

    private int GetPriority(char type)
    {
        var number = (int)type;

        return number > 96 ? number - 96 : number - 38;
    }
}