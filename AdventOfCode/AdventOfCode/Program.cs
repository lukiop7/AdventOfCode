// See https://aka.ms/new-console-template for more information

using AdventOfCode.Model;

var day = DayFactory.Create("3", "2023");
day.Solve();

Console.WriteLine(day.PartASolution);
Console.WriteLine(day.PartBSolution);