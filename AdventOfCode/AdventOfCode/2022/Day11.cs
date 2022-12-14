using AdventOfCode.Model;
using System.Numerics;

namespace AdventOfCode._2022;

public class Day11 : Day
{
    public Day11(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var monkeysLines = Input.Chunk(7).ToList();
        var monkeys = new List<Monkey>();
        foreach (var monkey in monkeysLines)
        {
            monkeys.Add(new Monkey(monkey.Skip(1).ToArray()));
        }

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    var values = monkey.ExecuteRound();

                    if (values is null) break;

                    monkeys[values.Value.index].Items.Enqueue(values.Value.value);
                }
            }
        }

        return monkeys.OrderByDescending(x => x.Inspections).Select(x => x.Inspections).Take(2)
            .Aggregate((a, b) => a * b);
    }

    protected override decimal SolvePartB()
    {
        var monkeysLines = Input.Chunk(7).ToList();
        var monkeys = new List<Monkey>();
        foreach (var monkey in monkeysLines)
        {
            monkeys.Add(new Monkey(monkey.Skip(1).ToArray(), false));
        }

        var factor = monkeys
            .Select(m => m.Divider)
            .Aggregate((f1, f2) => f1 * f2);

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    var values = monkey.ExecuteRound(factor);

                    if (values is null) break;

                    monkeys[values.Value.index].Items.Enqueue(values.Value.value);
                }
            }
        }

        Console.WriteLine(monkeys.OrderByDescending(x => x.Inspections).Select(x => x.Inspections).Take(2)
            .Aggregate((a, b) => a * b));
        return 0;
    }

    public class Monkey
    {
        public Queue<ulong> Items = new();
        public ulong Inspections = 0;
        private string _operation;
        public ulong Divider;
        private int _trueMonkeyIndex;
        private int _falseMonkeyIndex;
        private bool _divide = true;

        public Monkey(string[] input, bool divide = true)
        {
            GetItems(input[0]);
            _operation = input[1].Split("=", StringSplitOptions.TrimEntries)[1];
            Divider = ulong.Parse(input[2].Split("by", StringSplitOptions.TrimEntries)[1]);
            _trueMonkeyIndex = int.Parse(input[3].Split("monkey", StringSplitOptions.TrimEntries)[1]);
            _falseMonkeyIndex = int.Parse(input[4].Split("monkey", StringSplitOptions.TrimEntries)[1]);
            _divide = divide;
        }

        public (ulong value, int index)? ExecuteRound(ulong lcm = 0)
        {
            if (Items.Count == 0) return null;

            var value = EvaluateOperation();

            if (lcm != 0) value %= lcm;

            var index = Test(value) ? _trueMonkeyIndex : _falseMonkeyIndex;

            return new ValueTuple<ulong, int>(value, index);
        }

        private void GetItems(string line)
        {
            var split = line.Split(":", StringSplitOptions.TrimEntries);
            split = split[1].Split(",", StringSplitOptions.TrimEntries);
            var numbers = split.Select(ulong.Parse).ToList();
            foreach (var number in numbers)
            {
                Items.Enqueue(number);
            }
        }

        private ulong EvaluateOperation()
        {
            Inspections++;
            var item = Items.Dequeue();
            var split = _operation.Split(" ", StringSplitOptions.TrimEntries);
            ulong a;
            ulong b;
            if (!ulong.TryParse(split[0], out a))
                a = item;
            if (!ulong.TryParse(split[2], out b))
                b = item;

            return _divide ? ((split[1] == "*" ? a * b : a + b) / 3) : ((split[1] == "*" ? a * b : a + b));
        }

        private bool Test(ulong item) => BigInteger.Remainder(item, Divider) == 0;
    }
}