using Microsoft.CodeAnalysis;

namespace Exercism.Representers.CSharp.Simplification
{
    internal static class SyntaxNodeExtensions
    {
        public static bool IsEquivalentWhenNormalized(this SyntaxNode node, SyntaxNode other) =>
            SyntaxNodeComparer.IsEquivalentToNormalized(node, other);
    }
}