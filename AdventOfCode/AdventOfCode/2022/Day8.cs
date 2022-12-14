using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day8 : Day
{
    public Day8(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var array = Input.Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

        var count = 0;
        for (var i = 0; i < array.Length; i++)
        {
            for (var j = 0; j < array[i].Length; j++)
            {
                if (CheckCondition(j, i, array[i].Length, array.Length, array))
                    count++;
            }
        }

        return count;
    }

    private bool CheckCondition(int x, int y, int columns, int rows, int[][] input)
    {
        var result = true;

        if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1)
            return result;

        var iterator = x + 1;
        var value = input[x][y];
        while (iterator < columns)
        {
            if (value <= input[y][iterator])
            {
                result = false;
                break;
            }

            result = true;
            iterator++;
        }

        if (result) return true;

        iterator = x - 1;
        while (iterator >= 0)
        {
            if (value <= input[y][iterator])
            {
                result = false;
                break;
            }

            result = true;
            iterator--;
        }

        if (result) return true;

        iterator = y + 1;
        while (iterator < rows)
        {
            if (value <= input[iterator][x])
            {
                result = false;
                break;
            }

            result = true;
            iterator++;
        }

        if (result) return true;

        iterator = y - 1;
        while (iterator >= 0)
        {
            if (value <= input[iterator][x])
            {
                result = false;
                break;
            }

            result = true;
            iterator--;
        }

        return result;
    }

    protected override decimal SolvePartB()
    {
        var array = Input.Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

        var count = 0;
        for (var i = 0; i < array.Length; i++)
        {
            for (var j = 0; j < array[i].Length; j++)
            {
                var current = GetScenicScore(j, i, array[i].Length, array.Length, array);
                if (current > count)
                    count = current;
            }
        }

        return count;
    }

    private int GetScenicScore(int x, int y, int columns, int rows, int[][] input)
    {
        var result = 1;

        if (x == 0 || x == columns - 1 || y == 0 || y == rows - 1)
            return 0;

        var iterator = x + 1;
        var value = input[y][x];
        var counter = 0;
        while (iterator < columns)
        {
            if (value <= input[y][iterator])
            {
                counter++;
                break;
            }

            counter++;
            iterator++;
        }

        result *= counter;
        if (result == 0) return 0;

        iterator = x - 1;
        counter = 0;
        while (iterator >= 0)
        {
            if (value <= input[y][iterator])
            {
                counter++;

                break;
            }

            counter++;
            iterator--;
        }

        result *= counter;
        if (result == 0) return 0;

        iterator = y + 1;
        counter = 0;
        while (iterator < rows)
        {
            if (value <= input[iterator][x])
            {
                counter++;

                break;
            }

            counter++;
            iterator++;
        }

        result *= counter;
        if (result == 0) return 0;

        iterator = y - 1;
        counter = 0;
        while (iterator >= 0)
        {
            if (value <= input[iterator][x])
            {
                counter++;

                break;
            }

            counter++;
            iterator--;
        }

        result *= counter;
        return result;
    }
}