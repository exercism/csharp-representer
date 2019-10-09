using CommandLine;
using Serilog;

namespace Exercism.Representers.CSharp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Logging.Configure();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(CreateRepresentation);
        }

        private static void CreateRepresentation(Options options)
        {
            Log.Information("Creating representation for {Exercise} solution in directory {Directory}", options.Slug, options.Directory);

            var solution = SolutionParser.Parse(options);
            var representation = SolutionRepresenter.Represent(solution);
            RepresentationWriter.WriteToFile(options, representation);

            Log.Information("Created representation for {Exercise} solution in directory {Directory}", options.Slug, options.Directory);
        }
    }
}