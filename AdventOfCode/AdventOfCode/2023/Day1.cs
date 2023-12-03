using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023
{
    public class Day1 : Day
    {
        private static readonly Dictionary<string, int> _days = new Dictionary<string, int>
        {
            { "one" , 1},
            { "two" , 2},
            { "three" , 3},
            { "four" , 4},
            {"five", 5 },
            {"six", 6 },
            {"seven", 7 },
            {"eight", 8 },
            {"nine", 9 },
        };

        public Day1(string day, string year) : base(day, year)
        {
        }

        protected override decimal SolvePartA()
        {
            var count = 0;

            foreach (var item in Input)
            {
                var digits = item.Where(x => char.IsDigit(x)).ToArray();
                count += int.Parse($"{digits.First()}{digits.Last()}");
            }

            return count;
        }

        protected override decimal SolvePartB()
        {
            var count = 0;

            foreach (var item in Input)
            {
                var pattern = @"\d|" + string.Join("|", _days.Keys);
                var first = Regex.Match(item, pattern).Value;
                var last = Regex.Match(item, pattern, RegexOptions.RightToLeft).Value;

                var firstParsed = _days.ContainsKey(first) ? _days[first].ToString() : first;
                var lastParsed = _days.ContainsKey(last) ? _days[last].ToString() : last;
                count += int.Parse($"{firstParsed}{lastParsed}");
            }

            return count;
        }
    }
}
