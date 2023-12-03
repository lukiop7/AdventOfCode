using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023
{
    public class Day2 : Day
    {

        public Day2(string day, string year) : base(day, year)
        {
        }

        protected override decimal SolvePartA()
        {
            var count = 0;

            foreach(var line in Input)
            {
                var game = MapToGame(line);
                if (game.sets.Any(x => !x.IsValid(12, 13, 14))) continue;

                count += game.id;
            }

            return count;
        }

        protected override decimal SolvePartB()
        {
            var count = 0;

            foreach (var line in Input)
            {
                var game = MapToGame(line);

                var red = game.sets.MaxBy(x => x.red)?.red ?? 0;
                var blue = game.sets.MaxBy(x => x.blue)?.blue ?? 0;
                var green = game.sets.MaxBy(x => x.green)?.green ?? 0;

                count += (red*blue*green);
            }

            return count;
        }

        public record SetData(int red, int green, int blue) 
        {
            public bool IsValid(int redCount, int greenCount, int blueCount) =>
                red <= redCount && green <= greenCount && blue <= blueCount;
        }

        public record Game(int id, List<SetData> sets);
      
        private static SetData MapToSet(string line)
        {
            var cubes = line.Split(", ", StringSplitOptions.RemoveEmptyEntries);
            var red = 0;
            var green = 0;
            var blue = 0;

            foreach (var cube in cubes)
            {
                var cubeData = cube.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var value = int.Parse(cubeData[0]);

                switch (cubeData[1])
                {
                    case "red":
                        red = value; break;
                    case "blue":
                        blue = value; break;
                    case "green":
                        green = value; break;
                }
            }

            return new SetData(red, green, blue);
        }

        private static Game MapToGame(string line)
        {
            var split = line.Split(":", StringSplitOptions.RemoveEmptyEntries);
            var gameId = int.Parse(split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            var sets = split[1].Split("; ", StringSplitOptions.RemoveEmptyEntries).Select(x => MapToSet(x)).ToList();
            return new Game(gameId, sets);
        }
    }

}
