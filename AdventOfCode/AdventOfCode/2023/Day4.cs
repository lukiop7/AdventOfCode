using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023
{
    public class Day4 : Day
    {

        public Day4(string day, string year) : base(day, year)
        {
        }

        protected override decimal SolvePartA()
        {
            var count = 0;

            foreach (var item in Input)
            {
                var card = Parse(item);
                count += card.GetPoints();
            }

            return count;
        }

        protected override decimal SolvePartB()
        {
            var cards = new List<Card>();

            foreach (var item in Input)
            {
                var card = Parse(item);
                cards.Add(card);
            }

            var id = 1;
            var counts = cards.Select(_ => 1).ToList();

            while (id < Input.Count())
            {
                var item = cards.First(x => x.Id == id);

                for (var j = 0; j < counts[id - 1]; j++)
                {
                    for (var i = 0; i < item.Count; i++)
                    {
                        counts[id + i]++;
                    }
                }
                id++;
            }

            return counts.Sum();
        }

        private static Card Parse(string input)
        {
            var split = input.Split(':', '|');
            var id = int.Parse(split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);

            var winningNumbers = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var myNumbers = split[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var count = winningNumbers.Intersect(myNumbers).ToList().Count;

            return new Card(id, count);
        }

        private record Card(int Id, int Count)
        {
            public int GetPoints() => (int)Math.Pow(2, Count - 1);
        }
    }
}
