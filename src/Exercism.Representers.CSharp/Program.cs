using System;

namespace Exercism.Representers.CSharp;

internal record Options(string Slug, string InputDirectory, string OutputDirectory);

public static class Program
{
    public static void Main(string[] args)
    {
        var options = new Options(args[0], args[1], args[2]);
        CreateRepresentation(options);
    }

    private static void CreateRepresentation(Options options)
    {
        Console.WriteLine($"Creating representation for {options.Slug} solution in directory {options.InputDirectory}");

        var solution = SolutionParser.Parse(options);
        var (representation, mapping, concepts) = SolutionRepresenter.Represent(solution);
            
        RepresentationWriter.WriteToFile(options, representation, concepts);
        MappingWriter.WriteToFile(options, mapping);

        Console.WriteLine($"Created representation for {options.Slug} solution in directory {options.OutputDirectory}");
    }
}

internal static class StringExtensions
{
    internal static string Normalized(this string str) => str.TrimEnd().Replace("\r\n", "\n") + "\n";
}
