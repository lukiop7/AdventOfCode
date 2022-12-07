using AdventOfCode._2022;

namespace AdventOfCode.Model;

public static class DayFactory
{
    public static Day Create(string selector, string year)
    {
        var t = Type.GetType($"AdventOfCode._{year}.Day{selector}");
        return Activator.CreateInstance(t ?? throw new InvalidOperationException("typ"), selector, year) as Day ??
               throw new InvalidOperationException("dzien");
    }
}