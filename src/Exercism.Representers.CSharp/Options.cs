namespace Exercism.Representers.CSharp
{
    internal class Options
    {
        public string Slug { get; }
        public string InputDirectory { get; }
        public string OutputDirectory { get; }

        public Options(string slug, string inputDirectory, string outputDirectory) =>
            (Slug, InputDirectory, OutputDirectory) = (slug, inputDirectory, outputDirectory);
    }
}