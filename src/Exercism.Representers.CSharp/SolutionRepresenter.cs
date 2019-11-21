using System.Collections.Generic;
using Exercism.Representers.CSharp.Normalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Simplification;

namespace Exercism.Representers.CSharp
{
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

            var representation = new Representation(originalDocument.GetText(), simplifiedDocument.GetText());
            var mapping = new Mapping(identifiersToPlaceholders);
            return (representation, mapping);
        }

        private static string GetText(this TextDocument document) =>
            document.GetTextAsync().GetAwaiter().GetResult().ToString();
    }
}