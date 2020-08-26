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
            if (semanticModel == null)
            {
                return base.VisitInitializerExpression(node);
            }
            try
            {
                if (semanticModel.GetOperation(node) is IObjectOrCollectionInitializerOperation initializerOperation)
                {
                    if (initializerOperation.Parent?.Type?.Name != "Dictionary")
                    {
                        if (initializerOperation.Parent?.Type?.Name == null)
                        {
                            Log.Error($"{nameof(NormalizeDictionaryInitialization)}: unable to retrieve the type information for {nameof(initializerOperation)}");
                        }

                        return base.VisitInitializerExpression(node);
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
                        return base.VisitInitializerExpression(node);
                    }

                    if (!initializerDetails.Success)
                    {
                        Log.Error(
                            $"{nameof(NormalizeDictionaryInitialization)}: dictionary initialization found with incorrect number of valid arguments (!=2)");
                        return base.VisitInitializerExpression(node);
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
                        .First(n => n is InitializerExpressionSyntax);
                    return replacementNode;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(NormalizeDictionaryInitialization)}: unknown error");
            }

            return base.VisitInitializerExpression(node);
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> Initializers)
            ExtractInitializersWithObjectSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            var initializers = new List<KeyValuePair<SyntaxNode, SyntaxNode>>();
            foreach (var initializer in initializerOperation.Initializers)
            {
                var arg0 = initializer
                    .Descendants()
                    .FirstOrDefault(d => d is IArgumentOperation);
                var arg1 = (initializer as IAssignmentOperation)?.Value;
                if (arg0 == null || arg1 == null || arg0.Syntax == null || arg1.Syntax == null)
                {
                    return (false, null);
                }

                initializers.Add(new KeyValuePair<SyntaxNode, SyntaxNode>(arg0.Syntax, arg1.Syntax));
            }

            return (true, initializers);
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> Initializers)
            ExtractInitializersWithCollectionSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            var initializers = new List<KeyValuePair<SyntaxNode, SyntaxNode>>();
            foreach (var initializer in initializerOperation.Initializers)
            {
                var args = (initializer as IInvocationOperation)?.Arguments;
                if (args == null || args?.Length != 2 || args?[0].Syntax == null || args?[1].Syntax == null)
                {
                    return (false, null);
                }

                initializers.Add(new KeyValuePair<SyntaxNode, SyntaxNode>(args?[0].Syntax, args?[1].Syntax));
            }

            return (true, initializers);
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