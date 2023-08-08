using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        var representation = new Representation(new RepresentationText(originalDocument.GetText(), simplifiedDocument.GetText()), new RepresentationMetadata(1));
        var mapping = new Mapping(identifiersToPlaceholders.ToDictionary(kv => kv.Value, kv => kv.Key));
        return (representation, mapping);
    }

    private static string GetText(this TextDocument document) =>
        document.GetTextAsync().GetAwaiter().GetResult().ToString();
}

internal static class RepresentationWriter
{
    public static void WriteToFile(Options options, Representation representation)
    {
        File.WriteAllText(GetRepresentationTextFilePath(options), representation.ToRepresentationText());
        File.WriteAllText(GetRepresentationJsonFilePath(options), representation.ToRepresentationJson());
    }

    private static string ToRepresentationText(this Representation representation) =>
        representation.Text.Simplified.WithTrailingEmptyLine();

    private static string ToRepresentationJson(this Representation representation)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        writer.WriteStartObject();
        writer.WriteNumber("version", representation.Metadata.Version);
        writer.WriteEndObject();
        writer.Flush();

        return Encoding.UTF8.GetString(stream.ToArray()).WithTrailingEmptyLine();
    }

    private static string GetRepresentationTextFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.txt"));

    private static string GetRepresentationJsonFilePath(Options options) =>
        Path.GetFullPath(Path.Combine(options.OutputDirectory, "representation.json"));
}