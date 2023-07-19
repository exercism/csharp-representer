using System.IO;
using System.Text;
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

        private static string ToRepresentationJson(this Representation representation)
        {
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream);

            writer.WriteStartObject();
            writer.WriteNumber("version", representation.Metadata.Version);
            writer.WriteEndObject();
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }

        private static string GetRepresentationTextFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.txt"));

        private static string GetRepresentationJsonFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.json"));
    }
}