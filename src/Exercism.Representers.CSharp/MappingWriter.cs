using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp
{
    internal static class MappingWriter
    {
        public static void WriteToFile(Options options, Mapping mapping) =>
            File.WriteAllText(GetMappingFilePath(options), SerializeMapping(mapping));

        private static string GetMappingFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.OutputDirectory, "mapping.json"));

        private static string SerializeMapping(Mapping mapping) =>
            JsonSerializer.Serialize(mapping.PlaceholdersToIdentifier, new JsonSerializerOptions { WriteIndented = true });
    }
}