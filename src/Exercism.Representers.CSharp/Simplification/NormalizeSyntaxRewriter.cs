using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Simplification
{
    // TODO: check if this can be removed
    internal class NormalizeSyntaxRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode Visit(SyntaxNode node)
        {
            if (node == null)
                return null;

            var normalizedNode = node.WithoutTrivia().NormalizeWhitespace();
            return base.Visit(normalizedNode);
        }
    }
}