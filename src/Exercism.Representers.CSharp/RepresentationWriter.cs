using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp
{
    internal static class RepresentationWriter
    {
        public static void WriteToFile(Options options, Representation representation)
        {
            File.WriteAllText(GetRepresentationTextFilePath(options), representation.ToRepresentationText());
            File.WriteAllText(GetRepresentationJsonFilePath(options), representation.ToRepresentationJson());
        }
        
        private static string ToRepresentationText(this Representation representation) =>
            representation.Text.Simplified;

        private static string ToRepresentationJson(this Representation representation) =>
            JsonSerializer.Serialize(representation.Metadata, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        private static string GetRepresentationTextFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.txt"));

        private static string GetRepresentationJsonFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.json"));
    }
}