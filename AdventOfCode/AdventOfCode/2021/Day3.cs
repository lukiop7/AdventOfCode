using AdventOfCode.Model;

namespace AdventOfCode._2021;

public class Day3 : Day
{
    public Day3(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var ints = new int[Input[0].Length];

        for (var i = 1; i < Input.Length; i++)
        {
            for (var j = 1; j < Input[0].Length; j++)
            {
                ints[j] += Input[i][j] == '0' ? -1 : 1;
            }
        }

        var gamma = "";
        var epsilon = "";

        foreach (var bit in ints)
        {
            gamma += bit >= 0 ? '1' : '0';
            epsilon += bit >= 0 ? '0' : '1';
        }

        return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
    }

    protected override decimal SolvePartB()
    {
        return 0;
    }
}