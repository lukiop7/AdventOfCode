using System.Diagnostics;
using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day2 : Day
{
    public Day2(string day, string year) : base(day, year)
    {
    }

    private const char Rock = 'A';
    private const char Paper = 'B';
    private const char Scissors = 'C';
    private const int Offset = 'X' - 'A';


    protected override decimal SolvePartA()
    {
        return Input
            .Sum(x => GetPoints(x));
    }

    protected override decimal SolvePartB()
    {
        return Input
            .Sum(x => GetPoints(x, true));
    }


    private int GetPoints(string input, bool outcome = false)
    {
        var opponent = input[0];
        char player;

        if (outcome)
        {
            player = GetChoiceForOutcome(opponent, input[2]);
        }
        else
        {
            player = (char)(input[2] - Offset);
        }

        return GetPointsFromChoice(player) + GetPointsFromOutcome(opponent, player);
    }

    private int GetPointsFromChoice(char choice) => choice switch
    {
        Rock => 1,
        Paper => 2,
        Scissors => 3,
        _ => throw new ArgumentOutOfRangeException(nameof(choice), choice, null)
    };

    private int GetPointsFromOutcome(char opponent, char player)
    {
        if (opponent == player) return 3;

        if (opponent == Rock)
        {
            return player == Paper ? 6 : 0;
        }

        if (opponent == Scissors)
        {
            return player == Rock ? 6 : 0;
        }

        return player == Scissors ? 6 : 0;
    }

    private char GetChoiceForOutcome(char opponent, char outcome)
    {
        if (outcome == 'Y') return opponent;

        if (outcome == 'X')
        {
            return opponent switch
            {
                Rock => Scissors,
                Paper => Rock,
                Scissors => Paper,
                _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
            };
        }

        return opponent switch
        {
            Scissors => Rock,
            Rock => Paper,
            Paper => Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null)
        };
    }
}