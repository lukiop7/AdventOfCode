using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day5 : Day
{
    public Day5(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var stacks = Input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList();
        var instructions = Input.Skip(stacks.Count + 1).ToList();
        var parsedStacks = ParseStacks(stacks.SkipLast(1).ToList());
        foreach (var line in instructions)
            ExecuteInstruction(line, parsedStacks);
        var a = parsedStacks.Select(x => x.Pop());
        Console.WriteLine(string.Concat(a));
        return 0;
    }

    protected override decimal SolvePartB()
    {
        var stacks = Input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList();
        var instructions = Input.Skip(stacks.Count + 1).ToList();
        var parsedStacks = ParseStacks(stacks.SkipLast(1).ToList());
        foreach (var line in instructions)
            ExecuteInstructionOrder(line, parsedStacks);
        var a = parsedStacks.Select(x => x.Pop());
        Console.WriteLine(string.Concat(a));
        return 0;
    }

    private void ExecuteInstruction(string instruction, List<Stack<char>> stacks)
    {
        var split = instruction.Split(" ");
        var number = int.Parse(split[1]);
        var source = int.Parse(split[3]) - 1;
        var destination = int.Parse(split[5]) - 1;

        for (var i = 0; i < number; i++)
        {
            var popped = stacks[source].Pop();
            stacks[destination].Push(popped);
        }
    }

    private void ExecuteInstructionOrder(string instruction, List<Stack<char>> stacks)
    {
        var split = instruction.Split(" ");
        var number = int.Parse(split[1]);
        var source = int.Parse(split[3]) - 1;
        var destination = int.Parse(split[5]) - 1;

        var order = "";
        for (var i = 0; i < number; i++)
        {
            var popped = stacks[source].Pop();
            order += popped;
        }

        for (var i = order.Length - 1; i >= 0; i--)
        {
            stacks[destination].Push(order[i]);
        }
    }

    private List<Stack<char>> ParseStacks(List<string> stacks)
    {
        var result = new List<Stack<char>>();
        for (var i = stacks.Count - 1; i >= 0; i--)
        {
            var counter = 0;
            for (var j = 1; j < stacks[i].Length; j += 4)
            {
                if (result.ElementAtOrDefault(counter) == null)
                {
                    result.Add(new Stack<char>());
                }

                if (!char.IsWhiteSpace(stacks[i][j]))
                    result[counter].Push(stacks[i][j]);
                counter++;
            }
        }

        return result;
    }
}