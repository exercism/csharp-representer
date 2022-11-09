using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp
{
    internal static class RepresentationWriter
    {
        public static void WriteToFile(Options options, Representation representation)
        {
            File.WriteAllText(GetRepresentationTextFilePath(options), representation.Text.Simplified);
            File.WriteAllText(GetRepresentationJsonFilePath(options), JsonSerializer.Serialize(representation.Metadata));
        }

        private static string GetRepresentationTextFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.txt"));

        private static string GetRepresentationJsonFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.json"));
    }
}