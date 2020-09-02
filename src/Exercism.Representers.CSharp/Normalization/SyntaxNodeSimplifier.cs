using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Serilog;

namespace Exercism.Representers.CSharp.Normalization
{
    internal static class SyntaxNodeSimplifier
    {
        public static SyntaxNode Simplify(this SyntaxNode node, Dictionary<string, string> mapping)
        {
            var sm = CreateSemanticModel(node);
            return SyntaxRewriters(mapping, sm).Aggregate(node, (acc, rewriter) => rewriter.Visit(acc));
        }

        private static SemanticModel CreateSemanticModel(SyntaxNode node)
        {
            var tree = node.SyntaxTree;
            var refAssemblies = ((string)System.AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES"))
                .Split(System.IO.Path.PathSeparator)
                .Select(assemblyPath => MetadataReference.CreateFromFile(assemblyPath))
                .OfType<MetadataReference>()
                .ToArray();
            CSharpCompilation cmp = CSharpCompilation.Create(
                "FakeCode", new[] {tree}).AddReferences(refAssemblies);

            var sm = cmp.GetSemanticModel(tree);
            if (sm == null)
            {
                Log.Error($"{nameof(SyntaxNodeSimplifier)}: failed to create semantic model");
            }
            return sm;
        }

        private static CSharpSyntaxRewriter[] SyntaxRewriters(Dictionary<string, string> mapping, SemanticModel semanticModel)
            => new CSharpSyntaxRewriter[]
        {
            // rewriting using the semantic model must precede the other rewriters.
            // Presumably the syntax node hierarchy gets out of sync with the semantic model
            // as parts are rewritten so the normalization is not correct.
            //
            // Naively attempting to create the semantic model from the updated syntax node hierarchy
            // fails to generate semantic operations on which semantic rewriting depends. This is
            // most likely because the code is no longer correct for compilation but this
            // hypothesis has not been tested.
            new NormalizeDictionaryInitialization(semanticModel), 
            //
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
            new NormalizeWhiteSpace(),
        };
    }
}
