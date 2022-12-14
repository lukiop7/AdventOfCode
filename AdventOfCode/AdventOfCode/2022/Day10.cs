using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day10 : Day
{
    public Day10(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var cpu = new Cpu();
        foreach (var line in Input)
        {
            cpu.ExecuteCommand(line);
        }

        return cpu.GetSignalStrength();
    }

    protected override decimal SolvePartB()
    {
        Console.Clear();
        var cpu = new Cpu();
        foreach (var line in Input)
        {
            cpu.ExecuteCommand(line);
        }

        return 0;
    }


    public class Cpu
    {
        public Cpu()
        {
            IncrementCycle();
        }

        public List<int> Cycles = new();
        public int Register = 1;
        public int CrtColumn = 0;
        public int CrtRow = 0;
        public const int CrtWidth = 40;
        public const int CrtHeight = 6;

        public void ExecuteCommand(string command)
        {
            var split = command.Split(" ");

            if (split[0] == "noop")
            {
                IncrementCycle();
            }
            else
            {
                IncrementCycle();
                Register += int.Parse(split[1]);
                IncrementCycle();
            }
        }

        private void IncrementCycle()
        {
            DrawCrt();
            Cycles.Add(Register);
        }

        public int GetSignalStrength()
        {
            var signalStrength = 0;
            for (int i = 19; i < 220; i = i + 40)
            {
                signalStrength += (i + 1) * Cycles[i];
            }

            return signalStrength;
        }

        public void DrawCrt()
        {
            if (CrtColumn <= Register + 1 && CrtColumn >= Register - 1)
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }

            CrtColumn++;
            if (CrtColumn == CrtWidth)
            {
                Console.WriteLine();
                CrtColumn = 0;
            }
        }
    }
}