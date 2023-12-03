using AdventOfCode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2023
{
    // I'm not proud of my solutions :( 
    public class Day3 : Day
    {

        public Day3(string day, string year) : base(day, year)
        {
        }

        protected override decimal SolvePartA()
        {
            var count = 0;
            var array = Input.Select(x => x.Select(y => y).ToArray()).ToArray();
            var numbersIndices = new HashSet<(int y, int x)>();

            for(var y =  1; y < array.Length-1; y++)
            {
                for (var x = 1; x < array[y].Length-1; x++)
                {
                    var character = array[y][x];
                    if (!char.IsDigit(character) && character != '.')
                    {
                        // down
                        ProcessCheck(numbersIndices, array, y + 1, x);
                        // up
                        ProcessCheck(numbersIndices, array, y - 1, x);
                        // left
                        ProcessCheck(numbersIndices, array, y, x - 1);
                        // right
                        ProcessCheck(numbersIndices, array, y, x + 1);

                        // upper left
                        ProcessCheck(numbersIndices, array, y - 1, x - 1);
                        // upper right
                        ProcessCheck(numbersIndices, array, y - 1, x + 1);
                        // bottom left
                        ProcessCheck(numbersIndices, array, y + 1, x - 1);
                        // bottom right
                        ProcessCheck(numbersIndices, array, y + 1, x + 1);
                    }
                }
            }

            foreach(var indices in numbersIndices)
            {
                var stringed = new string(array[indices.y]).Substring(indices.x);
                var number = int.Parse(stringed.TakeWhile(x => char.IsDigit(x)).ToArray());
                count += number;
            }

            return count;
        }

        protected override decimal SolvePartB()
        {
            var count = 0;
            var array = Input.Select(x => x.Select(y => y).ToArray()).ToArray();

            for (var y = 1; y < array.Length - 1; y++)
            {
                for (var x = 1; x < array[y].Length - 1; x++)
                {
                    var character = array[y][x];
                    if (!char.IsDigit(character) && character != '.')
                    {
                        var numbers = new HashSet<(int y, int x)>();
                        // down
                        ProcessCheck(numbers, array, y + 1, x);
                        // up
                        ProcessCheck(numbers, array, y - 1, x);
                        // left
                        ProcessCheck(numbers, array, y, x - 1);
                        // right
                        ProcessCheck(numbers, array, y, x + 1);

                        // upper left
                        ProcessCheck(numbers, array, y - 1, x - 1);
                        // upper right
                        ProcessCheck(numbers, array, y - 1, x + 1);
                        // bottom left
                        ProcessCheck(numbers, array, y + 1, x - 1);
                        // bottom right
                        ProcessCheck(numbers, array, y + 1, x + 1);

                        if (character == '*' && numbers.Count >= 2)
                        {
                            var ratio = 1;
                            foreach (var indices in numbers)
                            {
                                var stringed = new string(array[indices.y]).Substring(indices.x);
                                var number = int.Parse(stringed.TakeWhile(x => char.IsDigit(x)).ToArray());
                                ratio *= number;
                            }

                            count += ratio;
                        }
                    }
                }
            }

            return count;
        }

        private void ProcessCheck(HashSet<(int y, int x)> numbersIndices, char[][] array, int y, int x)
        {
            if (char.IsDigit(array[y][x])) numbersIndices.Add((y, FindStartingIndex(array[y], x)));
        }


        private int FindStartingIndex(char[] line, int x)
        {
            if(x == 0) return 0;
            var index = x;
            while (char.IsDigit(line[index]))
            {
                if (--index == -1)
                    return 0;
            }

            return index + 1;
        }
    }

}
