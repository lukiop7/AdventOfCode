using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day1 : Day
{
    public Day1(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var counter = 0;
        return Input
            .Select(x => new { Item = x, Group = !string.IsNullOrWhiteSpace(x) ? counter : counter++ })
            .Where(x => !string.IsNullOrWhiteSpace(x.Item))
            .GroupBy(x => x.Group)
            .Select(x => x.Sum(i => int.Parse(i.Item)))
            .Max();
    }

    protected override decimal SolvePartB()
    {
        var counter = 0;
        return Input
            .Select(x => new { Item = x, Group = !string.IsNullOrWhiteSpace(x) ? counter : counter++ })
            .Where(x => !string.IsNullOrWhiteSpace(x.Item))
            .GroupBy(x => x.Group)
            .Select(x => x.Sum(i => int.Parse(i.Item)))
            .OrderByDescending(x => x)
            .Take(3)
            .Sum();
    }
}