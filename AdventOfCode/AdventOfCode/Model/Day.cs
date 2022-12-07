using AdventOfCode.utils;

namespace AdventOfCode.Model;

public abstract class Day
{
    protected string[] Input { get; init; }

    public decimal PartASolution;

    public decimal PartBSolution;

    protected Day(string day, string year)
    {
        Input = InputReader.GetInputLines(day, year);
    }

    public void Solve()
    {
        PartASolution = SolvePartA();
        PartBSolution = SolvePartB();
    }

    protected abstract decimal SolvePartA();

    protected abstract decimal SolvePartB();
}