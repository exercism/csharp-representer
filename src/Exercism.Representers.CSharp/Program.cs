using System;

namespace Exercism.Representers.CSharp
{
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

            // var solution = SolutionParser.Parse(options);
            // var (representation, mapping) = SolutionRepresenter.Represent(solution);
            //
            // RepresentationWriter.WriteToFile(options, representation);
            // MappingWriter.WriteToFile(options, mapping);

            var representation = new Representation(new RepresentationText("Orig", "Simplified"), new RepresentationMetadata(2));
            RepresentationWriter.WriteToFile(options, representation);

            var mapping = new Mapping(new() {["Name"] = "Placeholder", ["Erik"] = "Cool"});
            MappingWriter.WriteToFile(options, mapping);

            Console.WriteLine($"Created representation for {options.Slug} solution in directory {options.OutputDirectory}");
        }
    }
}