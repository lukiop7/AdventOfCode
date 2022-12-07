namespace AdventOfCode.utils;

public static class InputReader
{
    private static readonly string
        BasePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\inputs"!;

    public static string[] GetInputLines(string day, string year) => File.ReadAllLines(@$"{BasePath}\{year}\{day}.txt");
}