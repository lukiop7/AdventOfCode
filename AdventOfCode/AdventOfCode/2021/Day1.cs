using AdventOfCode.Model;

namespace AdventOfCode._2021;

public class Day1 : Day
{
    public Day1(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var counter = 0;
        for (var i = 1; i < Input.Length; i++)
        {
            if (int.Parse(Input[i]) > int.Parse(Input[i - 1]))
                counter++;
        }

        return counter;
    }

    protected override decimal SolvePartB()
    {
        var windows = new List<List<int>>
        {
            {
                new List<int>
                {
                    int.Parse(Input[0]),
                    int.Parse(Input[1])
                }
            },
            { new List<int> { int.Parse(Input[1]) } }
        };

        for (var i = 2; i < Input.Length; i++)
        {
            windows.Add(new List<int>());
            windows[i].Add(int.Parse(Input[i]));
            if (windows[i - 1].Count < 3)
                windows[i - 1].Add(int.Parse(Input[i]));
            if (windows[i - 2].Count < 3)
                windows[i - 2].Add(int.Parse(Input[i]));
        }

        var inputs = windows.Select(x => x.Sum()).ToList();
        var counter = 0;
        for (var i = 1; i < inputs.Count; i++)
        {
            if (inputs[i] > inputs[i - 1])
                counter++;
        }

        return counter;
    }
}