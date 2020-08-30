using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Serilog;

namespace Exercism.Representers.CSharp.Normalization
{
    public class NormalizeDictionaryInitialization : CSharpSyntaxRewriter
    {
        private readonly SemanticModel semanticModel;

        public NormalizeDictionaryInitialization(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            SyntaxNode DefaultVisit() => base.VisitInitializerExpression(node);

            if (semanticModel == null)
            {
                return DefaultVisit();
            }

            try
            {
                if (semanticModel.GetOperation(node) is IObjectOrCollectionInitializerOperation initializerOperation)
                {
                    if (initializerOperation.Parent?.Type?.Name != "Dictionary")
                    {
                        if (initializerOperation.Parent?.Type?.Name == null)
                        {
                            Log.Error(
                                $"{nameof(NormalizeDictionaryInitialization)}: unable to retrieve the type information for {nameof(initializerOperation)}");
                        }

                        return DefaultVisit();
                        // presumably a list or some object or other
                    }

                    (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> Initializers) initializerDetails;
                    if (node.Kind() == SyntaxKind.CollectionInitializerExpression)
                    {
                        initializerDetails = ExtractInitializersWithCollectionSyntax(initializerOperation);
                    }
                    else if (node.Kind() == SyntaxKind.ObjectInitializerExpression)
                    {
                        initializerDetails = ExtractInitializersWithObjectSyntax(initializerOperation);
                    }
                    else
                    {
                        Log.Error(
                            $"{nameof(NormalizeDictionaryInitialization)}: {nameof(InitializerExpressionSyntax)} found with unexpected Kind {node.Kind()}");
                        return DefaultVisit();
                    }

                    if (!initializerDetails.Success)
                    {
                        Log.Error(
                            $"{nameof(NormalizeDictionaryInitialization)}: dictionary initialization found with incorrect number of valid arguments (!=2)");
                        return DefaultVisit();
                    }

                    var explodedInitializers = new List<KeyValuePair<SyntaxNode, SyntaxNode>>();
                    foreach (var initializer in initializerDetails.Initializers)
                    {
                        explodedInitializers.Add(
                            new KeyValuePair<SyntaxNode, SyntaxNode>(this.Visit(initializer.Key)
                                , this.Visit(initializer.Value)));
                    }

                    var dictionaryAsText = BuildDictionaryAsText(explodedInitializers);
                    var dictionarySyntaxTree = CSharpSyntaxTree.ParseText(dictionaryAsText);
                    var replacementNode = dictionarySyntaxTree
                        .GetRoot()
                        .DescendantNodes()
                        .FirstOrDefault(n => n is InitializerExpressionSyntax);
                    if (replacementNode == default(SyntaxNode))
                    {
                        Log.Error(
                            $"{nameof(NormalizeDictionaryInitialization)}: failed to find a {nameof(InitializerExpressionSyntax)} node in generated dictionary fragment");
                        return DefaultVisit();
                    }

                    return replacementNode;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(NormalizeDictionaryInitialization)}: unknown error");
            }

            return DefaultVisit();
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> Initializers)
            ExtractInitializersWithObjectSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            try
            {
                var initializerSyntaxNodes = initializerOperation.Initializers
                    .Select(initializer => new
                    {
                        arg0 = initializer.Descendants().FirstOrDefault(d => d is IArgumentOperation),
                        arg1 = (initializer as IAssignmentOperation).Value
                    })
                    .Select(p => (p.arg0?.Syntax is null || p.arg1.Syntax is null)
                        ? throw new NullReferenceException()
                        : new KeyValuePair<SyntaxNode, SyntaxNode>(p.arg0.Syntax, p.arg1.Syntax))
                    .ToList();
                return (true, initializerSyntaxNodes);
            }
            catch (NullReferenceException)
            {
                return (false, null);
            }
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> Initializers)
            ExtractInitializersWithCollectionSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            try
            {
                var initializerSyntaxNodes = initializerOperation.Initializers
                    .Select(initializer => (initializer as IInvocationOperation)?.Arguments)
                    .Select(args =>
                        (args?.Length != 2 || args?[0]?.Syntax is null || args?[1]?.Syntax is null)
                            ? throw new NullReferenceException()
                            : new KeyValuePair<SyntaxNode, SyntaxNode>(args?[0]?.Syntax, args?[1]?.Syntax)
                    ).ToList();
                return (true, initializerSyntaxNodes);
            }
            catch (NullReferenceException)
            {
                return (false, null);
            }
        }

        private string BuildDictionaryAsText(IEnumerable<KeyValuePair<SyntaxNode, SyntaxNode>> initializerArgNodes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("var dict = new Dictionary{");
            foreach (var initializerArg in initializerArgNodes)
            {
                sb.AppendLine($"{{{initializerArg.Key.GetText()}, {initializerArg.Value.GetText()}}},");
            }

            sb.AppendLine("};");
            return sb.ToString();
        }
    }
}