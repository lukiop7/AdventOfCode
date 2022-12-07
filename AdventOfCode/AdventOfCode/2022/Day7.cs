using System.Security.Cryptography.X509Certificates;
using AdventOfCode.Model;

namespace AdventOfCode._2022;

public class Day7 : Day
{
    public Day7(string day, string year) : base(day, year)
    {
    }

    protected override decimal SolvePartA()
    {
        var system = new FileSystem();
        system.CurrentDirectory = system.BaseDirectory;

        foreach (var line in Input)
        {
            system.ExecuteCommand(line);
        }


        return Size(system.BaseDirectory);
    }

    public int Size(Directory system)
    {
        var visited = new HashSet<Directory>();

        var stack = new Stack<Directory>();
        stack.Push(system);
        var size = 0;

        while (stack.Count > 0)
        {
            var vertex = stack.Pop();

            if (visited.Contains(vertex))
                continue;

            visited.Add(vertex);

            foreach (var neighbor in vertex.Children)
                if (neighbor is Directory directory && !visited.Contains(neighbor))
                {
                    if (directory.Size <= 100000)
                        size += directory.Size;

                    stack.Push(directory);
                }
        }

        return size;
    }

    protected override decimal SolvePartB()
    {
        var system = new FileSystem();
        system.CurrentDirectory = system.BaseDirectory;

        foreach (var line in Input)
        {
            system.ExecuteCommand(line);
        }


        return Delete(system);
    }

    public int Delete(FileSystem fileSystem)
    {
        var system = fileSystem.BaseDirectory;
        var visited = new HashSet<Directory>();

        var stack = new Stack<Directory>();
        stack.Push(system);
        var smallestSize = int.MaxValue;

        while (stack.Count > 0)
        {
            var vertex = stack.Pop();

            if (visited.Contains(vertex))
                continue;

            visited.Add(vertex);

            foreach (var neighbor in vertex.Children)
                if (neighbor is Directory directory && !visited.Contains(neighbor))
                {
                    if (fileSystem.BaseDirectory.Size - directory.Size <= 40000000 && smallestSize > directory.Size)
                        smallestSize = directory.Size;

                    stack.Push(directory);
                }
        }

        return smallestSize;
    }

    public class FileSystem
    {
        public Directory BaseDirectory { get; set; } = new("/", null);
        public Directory? CurrentDirectory { get; set; }

        public void ExecuteCommand(string command)
        {
            var split = command.Split(" ");
            if (split[0] == "$")
            {
                if (split[1] == "cd") Cd(split[2]);
                return;
            }

            if (CurrentDirectory!.Children.All(x => x.Name != split[1]))
                CurrentDirectory.Children.Add(split[0] == "dir"
                    ? new Directory(split[1], CurrentDirectory)
                    : new File(split[1], int.Parse(split[0])));
        }

        public void Cd(string dir)
        {
            if (dir == "/") CurrentDirectory = BaseDirectory;
            else if (dir == "..") CurrentDirectory = CurrentDirectory!.Parent;
            else CurrentDirectory = CurrentDirectory!.Children.First(x => x.Name == dir) as Directory;
        }
    }

    public class Directory : IFile
    {
        public Directory(string name, Directory? parent)
        {
            Name = name;
            Parent = parent;
        }

        public string Name { get; init; }
        public int Size => Children.Sum(x => x.Size);
        public List<IFile> Children = new();
        public Directory? Parent { get; set; }
    }

    public class File : IFile
    {
        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; init; }
        public int Size { get; init; }
    }


    public interface IFile
    {
        string Name { get; init; }
        int Size { get; }
    }
}