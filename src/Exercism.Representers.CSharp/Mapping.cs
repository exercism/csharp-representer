using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp;

internal record Mapping(Dictionary<string, string> PlaceholdersToIdentifier);

internal static class MappingWriter
{
    private static readonly JsonWriterOptions JsonWriterOptions = new() {Indented = true};
    
    public static void WriteToFile(Options options, Mapping mapping)
    {
        using var fileStream = File.Create(GetMappingFilePath(options));
        using var jsonWriter = new Utf8JsonWriter(fileStream, JsonWriterOptions);
        jsonWriter.WriteStartObject();
            
        foreach (var (key, value) in mapping.PlaceholdersToIdentifier)
            jsonWriter.WriteString(key, value);

        jsonWriter.WriteEndObject();
        jsonWriter.Flush();
        fileStream.WriteByte((byte)'\n');
    }

    private static string GetMappingFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "mapping.json"));
}