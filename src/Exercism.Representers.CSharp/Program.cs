using CommandLine;
using Serilog;

namespace Exercism.Representers.CSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(CreateRepresentation);
        }

        private static void CreateRepresentation(Options options)
        {
            if (!options.SkipLogConfiguration)
            {
                Logging.Configure();
            }

            Log.Information("Creating representation for {Exercise} solution in directory {Directory}", options.Slug, options.InputDirectory);

            var solution = SolutionParser.Parse(options);
            var (representation, mapping) = SolutionRepresenter.Represent(solution);

            RepresentationWriter.WriteToFile(options, representation);
            MappingWriter.WriteToFile(options, mapping);

            Log.Information("Created representation for {Exercise} solution in directory {Directory}", options.Slug, options.OutputDirectory);
        }
    }
}