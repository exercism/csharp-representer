namespace Exercism.Representers.CSharp
{
    internal record Representation(RepresentationText Text, RepresentationMetadata Metadata);
    internal record RepresentationText(string Original, string Simplified);
    internal record RepresentationMetadata(int Version);
}