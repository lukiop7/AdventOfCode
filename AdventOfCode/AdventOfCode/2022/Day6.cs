using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day6 : Day
{
    public Day6(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var list = Input[0].ToList();
        for (var index = 0; index < Input[0].Length; index++)
        {
            if (list.GetRange(index, 4).Distinct().Count() == 4)
            {
                return index + 4;
            }
        }

        return 0;
    }

    protected override decimal SolvePartB()
    {
        var list = Input[0].ToList();
        for (var index = 0; index < Input[0].Length; index++)
        {
            if (list.GetRange(index, 14).Distinct().Count() == 14)
            {
                return index + 14;
            }
        }

        return 0;
    }
}