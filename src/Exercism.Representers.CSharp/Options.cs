using CommandLine;

namespace Exercism.Representers.CSharp
{
    internal class Options
    {
        [Value(0, Required = true, HelpText = "The solution's exercise")]
        public string Slug { get; }

        [Value(1, Required = true, HelpText = "The directory containing the solution")]
        public string InputDirectory { get; }

        [Value(2, Required = true, HelpText = "The directory to which the results will be written")]
        public string OutputDirectory { get; }

        [Option('s', "SkipLogConfiguration", Required = false, HelpText = "A flag (used for unit testing) to skip configuration of the logger")]
        public bool SkipLogConfiguration { get; }

        public Options(string slug, string inputDirectory,
            string outputDirectory, bool skipLogConfiguration = false) =>
            (Slug, InputDirectory, OutputDirectory, SkipLogConfiguration) 
            = (slug, inputDirectory, outputDirectory, skipLogConfiguration);
    }
}