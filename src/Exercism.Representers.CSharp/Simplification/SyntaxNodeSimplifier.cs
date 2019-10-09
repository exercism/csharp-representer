using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Simplification
{
    internal static class SyntaxNodeSimplifier
    {
        private static readonly CSharpSyntaxRewriter[] SyntaxRewriters =
        {
            // TODO: remove comments from syntax
            // TODO: remove unneeded parentheses from method call

            new RemoveComments(),
            new RemoveOptionalParentheses(),
            new SimplifyFullyQualifiedName(),
            new SimplifyBuiltInKeyword(),
            new InvertNegativeConditionals(),
            new AddBracesToIfAndElseStatements(),
            new LowerCaseToUpperCaseExponentNotation()
        };

        public static SyntaxNode Simplify(this SyntaxNode node) =>
            SyntaxRewriters.Aggregate(node, (acc, rewriter) => rewriter.Visit(acc));
    }
}