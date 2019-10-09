using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Simplification
{
    internal static class SyntaxNodeSimplifier
    {
        private static readonly CSharpSyntaxRewriter[] SyntaxRewriters =
        {
            // TODO: remove unneeded parentheses from method call

            new RemoveOptionalParentheses(),
            new SimplifyFullyQualifiedName(),
            new SimplifyBuiltInKeyword(),
            new InvertNegativeConditionals(),
            new AddBracesToIfAndElseStatements(),
            new LowerCaseToUpperCaseExponentNotation(),
            new RemoveTrivia()
        };

        public static SyntaxNode Simplify(this SyntaxNode node) =>
            SyntaxRewriters.Aggregate(node, (acc, rewriter) => rewriter.Visit(acc));
    }
}