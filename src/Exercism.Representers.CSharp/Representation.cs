using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Exercism.Representers.CSharp.Normalization;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Simplification;

namespace Exercism.Representers.CSharp;

internal record Representation(RepresentationText Text, RepresentationMetadata Metadata);

internal record RepresentationText(string Original, string Simplified);

internal record RepresentationMetadata(int Version);

internal static class SolutionRepresenter
{
    private const int Version = 2;
    
    public static (Representation representation, Mapping mapping) Represent(Solution solution)
    {
        var originalDocument = solution.Document;
        var syntax = originalDocument.GetSyntaxRootAsync().GetAwaiter().GetResult();
        var documentToSimplify = originalDocument.WithSyntaxRoot(syntax.WithAdditionalAnnotations(Simplifier.Annotation));

        var reducedDocument = Simplifier.ReduceAsync(documentToSimplify).GetAwaiter().GetResult();
        var reducedSyntax = reducedDocument.GetSyntaxRootAsync().GetAwaiter().GetResult();

        var identifiersToPlaceholders = new Dictionary<string, string>();
        var simplifiedSyntax = reducedSyntax.Simplify(identifiersToPlaceholders);
        var simplifiedDocument = originalDocument.WithSyntaxRoot(simplifiedSyntax);

        var representation = new Representation(new RepresentationText(originalDocument.GetText(), simplifiedDocument.GetText()), new RepresentationMetadata(Version));
        var mapping = new Mapping(identifiersToPlaceholders.ToDictionary(kv => kv.Value, kv => kv.Key));
        return (representation, mapping);
    }

    private static string GetText(this TextDocument document) =>
        document.GetTextAsync().GetAwaiter().GetResult().ToString();
}

internal static class RepresentationWriter
{
    private static readonly JsonWriterOptions JsonWriterOptions = new() {Indented = true};
    
    public static void WriteToFile(Options options, Representation representation)
    {
        WriteRepresentationToFile(options, representation);
        WriteMetadataToFile(options, representation);
    }

    private static void WriteRepresentationToFile(Options options, Representation representation) =>
        File.WriteAllText(GetRepresentationTextFilePath(options), representation.ToRepresentationText());

    private static string ToRepresentationText(this Representation representation) =>
        representation.Text.Simplified.ReplaceLineEndings("\n") + "\n";

    private static string GetRepresentationTextFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.txt"));

    private static void WriteMetadataToFile(Options options, Representation representation)
    {
        using var fileStream = File.Create(GetRepresentationJsonFilePath(options));
        using var jsonWriter = new Utf8JsonWriter(fileStream, JsonWriterOptions);

        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber("version", representation.Metadata.Version);
        jsonWriter.WriteEndObject();
        jsonWriter.Flush();
        fileStream.WriteByte((byte)'\n');
    }

    private static string GetRepresentationJsonFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.json"));
}