using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Normalization
{
    internal static class SyntaxNodeSimplifier
    {
        public static SyntaxNode Simplify(this SyntaxNode node, Dictionary<string, string> mapping) 
            => SyntaxRewriters(mapping).Aggregate(node, (acc, rewriter) => rewriter.Visit(acc));

        private static CSharpSyntaxRewriter[] SyntaxRewriters(Dictionary<string, string> mapping)
            => new CSharpSyntaxRewriter[]
        {
            new RemoveOptionalParentheses(),
            new SimplifyFullyQualifiedName(),
            new SimplifyBuiltInKeyword(),
            new InvertNegativeConditionals(),
            new AddBracesToIfAndElseStatements(),
            new LowerCaseToUpperCaseExponentNotation(),
            new SimplifyBooleanEquality(),
            new RemoveReadOnly(),
            new RemoveUsingDirectives(),
            new RemoveComments(),
            new NormalizeIdentifiers(mapping),
            new NormalizeDictionaryInitialization(),
            // NormalizeWhiteSpace() must be last rewriter as
            // the unit test expected values have a whitespace dependency
            new NormalizeWhiteSpace(),
        };
    }
}
