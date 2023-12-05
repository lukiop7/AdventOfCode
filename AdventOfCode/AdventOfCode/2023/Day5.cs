using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023
{
    public class Day5 : Day
    {

        public Day5(string day, string year) : base(day, year)
        {
        }

        protected override decimal SolvePartA()
        {
            var count = 3;

            var seeds = Regex.Matches(Input[0], @"\d+").Select(x => ulong.Parse(x.Value)).ToList();
            var maps = new List<Map>();

            while(count < Input.Length)
            {
                var ranges = new List<Range>();
                while (count < Input.Length && !string.IsNullOrEmpty(Input[count]))
                {
                    var range = Parse(Input[count++]);
                    ranges.Add(range);
                }
                maps.Add(new Map(ranges));
                count += 2;
            }

            foreach (var map in maps)
            {
                seeds = seeds.Select(x => map.GetValue(x)).ToList();
            }

            return seeds.Min();
        }

        protected override decimal SolvePartB()
        {
            var count = 3;

            var seeds = Regex.Matches(Input[0], @"\d+").Select(x => ulong.Parse(x.Value)).ToList();

            var maps = new List<Map>();

            while (count < Input.Length)
            {
                var ranges = new List<Range>();
                while (count < Input.Length && !string.IsNullOrEmpty(Input[count]))
                {
                    var range = Parse(Input[count++]);
                    ranges.Add(range);
                }
                maps.Add(new Map(ranges));
                count += 2;
            }

            foreach (var map in maps)
            {
                seeds = seeds.Select(x => map.GetValue(x)).ToList();
            }

            return seeds.Min();
        }

        private Range Parse(string input)
        {
            var matches = Regex.Matches(input, @"\d+");
            var numbers = matches.Select(x => ulong.Parse(x.Value)).ToList();
            return new Range(numbers[0], numbers[1], numbers[2]);
        }

        private record Map(List<Range> Ranges)
        {
            public ulong GetValue(ulong value)
            {
                foreach(var range in Ranges)
                {
                    if (value < range.SourceStart || value >= range.SourceStart + range.Length)
                        continue;

                    return range.DestinationStart +  (value - range.SourceStart);
                }

                return value;
            }
        }

        private record Range(ulong DestinationStart, ulong SourceStart, ulong Length);
    }
}
