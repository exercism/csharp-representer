using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Simplification;

namespace Exercism.Representers.CSharp.Normalization
{
    internal static class DocumentSimplifier
    {
        public static Document Simplify(this Document document)
        {
            var syntax = document.GetSyntaxRootAsync().GetAwaiter().GetResult();
            var documentToSimplify = document.WithSyntaxRoot(syntax.WithAdditionalAnnotations(Simplifier.Annotation));

            var reducedDocument = Simplifier.ReduceAsync(documentToSimplify).GetAwaiter().GetResult();
            var reducedSyntax = reducedDocument.GetSyntaxRootAsync().GetAwaiter().GetResult();

            var simplifiedSyntax = reducedSyntax.Simplify();
            return document.WithSyntaxRoot(simplifiedSyntax);
        }
    }
}