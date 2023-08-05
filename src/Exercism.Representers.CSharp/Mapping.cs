using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Exercism.Representers.CSharp;

internal record Mapping(Dictionary<string, string> PlaceholdersToIdentifier);

internal static class MappingWriter
{
    public static void WriteToFile(Options options, Mapping mapping) =>
        File.WriteAllText(GetMappingFilePath(options), SerializeMapping(mapping));

    private static string GetMappingFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "mapping.json"));

    private static string SerializeMapping(Mapping mapping)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        writer.WriteStartObject();
            
        foreach (var (key, value) in mapping.PlaceholdersToIdentifier)
            writer.WriteString(key, value);

        writer.WriteEndObject();
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray());
    }
}