using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Microsoft.CodeAnalysis.Operations;
using Serilog;

namespace Exercism.Representers.CSharp.Normalization
{
    public class NormalizeDictionaryInitialization : CSharpSyntaxRewriter
    {
        private class InvalidKindException : Exception
        {
            public SyntaxKind Kind;
        }

        private readonly SemanticModel semanticModel;

        public NormalizeDictionaryInitialization(SemanticModel semanticModel)
        {
            if (semanticModel == default)
            {
                Log.Error(
                    $"{nameof(NormalizeDictionaryInitialization)}: semantic model is null - this normalization will fail");
            }

            this.semanticModel = semanticModel;
        }

        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            SyntaxNode DefaultVisit() => base.VisitInitializerExpression(node);

            if (semanticModel == default)
            {
                return DefaultVisit();
            }

            try
            {
                var initializerOperation = semanticModel.GetOperation(node) as IObjectOrCollectionInitializerOperation;
                if (initializerOperation == default)
                {
                    return DefaultVisit();
                }

                if (!IsDictionary(initializerOperation))
                {
                    return DefaultVisit();
                }

                var initializerExtractionResults
                    = node.Kind() switch
                    {
                        SyntaxKind.CollectionInitializerExpression
                        => ExtractInitializersWithCollectionSyntax(initializerOperation),
                        SyntaxKind.ObjectInitializerExpression
                        => ExtractInitializersWithObjectSyntax(initializerOperation),
                        _
                        => throw new InvalidKindException {Kind = node.Kind()}
                    };

                if (!initializerExtractionResults.Success)
                {
                    return DefaultVisit();
                }

                var visitedInitializerSyntaxNodes = initializerExtractionResults.InitializerSyntaxNodes
                    .Select(initializer => new KeyValuePair<SyntaxNode, SyntaxNode>(
                        this.Visit(initializer.Key),
                        this.Visit(initializer.Value))
                    );

                var replacementNode
                    = BuildReplacementSyntaxWithCollectionInitialization(node.Kind(), visitedInitializerSyntaxNodes);
                
                if (replacementNode == default)
                {
                    Log.Error(
                        $"{nameof(NormalizeDictionaryInitialization)}: failed to find a {nameof(InitializerExpressionSyntax)} node in generated dictionary fragment");
                    return DefaultVisit();
                }

                return base.VisitInitializerExpression(replacementNode as InitializerExpressionSyntax);
            }
            catch (InvalidKindException ike)
            {
                Log.Error(
                    $"{nameof(NormalizeDictionaryInitialization)}: {nameof(InitializerExpressionSyntax)} found with unexpected Kind {ike.Kind}");
            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(NormalizeDictionaryInitialization)}: unknown error");
            }

            return DefaultVisit();
        }

        private bool IsDictionary(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            if (initializerOperation.Parent?.Type?.Name != "Dictionary")
            {
                if (initializerOperation.Parent?.Type?.Name == default)
                {
                    Log.Error(
                        $"{nameof(NormalizeDictionaryInitialization)}: unable to retrieve the type information for {nameof(initializerOperation)}");
                }

                return false;
                // presumably a list or some object or other
            }

            return true;
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodes)
            ExtractInitializersWithObjectSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            try
            {
                var initializerSyntaxNodes = initializerOperation.Initializers
                    .Select(initializer => new
                    {
                        arg0 = initializer.Descendants().FirstOrDefault(d => d is IArgumentOperation),
                        arg1 = (initializer as IAssignmentOperation)?.Value
                    })
                    .Select(p => (p.arg0?.Syntax == default || p.arg1?.Syntax == default)
                        ? throw new NullReferenceException(p.arg0?.Syntax == default
                            ? "arg0 is invalid"
                            : "arg1 is invalid" + $" in '{initializerOperation.Syntax}'")
                        : new KeyValuePair<SyntaxNode, SyntaxNode>(p.arg0.Syntax, p.arg1.Syntax))
                    .ToList();
                return (true, initializerSyntaxNodes);
            }
            catch (NullReferenceException nre)
            {
                Log.Error(nre,
                    $"{nameof(NormalizeDictionaryInitialization)}.{nameof(ExtractInitializersWithObjectSyntax)}: dictionary initialization found with incorrect number of valid arguments (!=2)");
                return (false, default);
            }
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodes)
            ExtractInitializersWithCollectionSyntax(IObjectOrCollectionInitializerOperation initializerOperation)
        {
            string ErrorMessage(IObjectOrCollectionInitializerOperation op,
                ImmutableArray<IArgumentOperation>? args) =>
                args switch
                {
                    _ when args == default => "args is null",
                    _ when args?.Length != 2 => $"invalid number of arguments: {args?.Length}",
                    _ when args?[0]?.Syntax == default && args?[1]?.Syntax == default
                    => "args are both invalid",
                    _ when args?[0]?.Syntax == default => "arg 0 is invalid",
                    _ => "arg 1 is invalid"
                } + $" in '{op.Syntax}'";

            try
            {
                var initializerSyntaxNodes = initializerOperation.Initializers
                    .Select(initializer => (initializer as IInvocationOperation)?.Arguments)
                    .Select(args =>
                        (args?.Length != 2 || args?[0]?.Syntax == default || args?[1]?.Syntax == default)
                            ? throw new NullReferenceException(ErrorMessage(initializerOperation, args))
                            : new KeyValuePair<SyntaxNode, SyntaxNode>(args?[0]?.Syntax, args?[1]?.Syntax)
                    ).ToList();
                return (true, initializerSyntaxNodes);
            }
            catch (NullReferenceException nre)
            {
                Log.Error(nre,
                    $"{nameof(NormalizeDictionaryInitialization)}.{nameof(ExtractInitializersWithObjectSyntax)}: dictionary initialization found with incorrect number of valid arguments (!=2)");
                return (false, default);
            }
        }


        private SyntaxNode BuildReplacementSyntaxWithCollectionInitialization(SyntaxKind initializationKind
            ,IEnumerable<KeyValuePair<SyntaxNode, SyntaxNode>> visitedInitializerSyntaxNodes)
        {
            var KeyExtractor
                = initializationKind == SyntaxKind.CollectionInitializerExpression
                ? (Func<KeyValuePair<SyntaxNode,SyntaxNode>, SyntaxNode>)(p => p.Key)
                : p => p.Key.ChildNodes().FirstOrDefault();

            var initializerTree = new List<SyntaxNodeOrToken>();

            foreach (var pair in visitedInitializerSyntaxNodes)
            {
                var initializer = InitializerExpression(
                    SyntaxKind.ComplexElementInitializerExpression,
                    SeparatedList<ExpressionSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            KeyExtractor(pair),
                            Token(SyntaxKind.CommaToken),
                            pair.Value
                        }
                    ));
                initializerTree.Add(initializer);
                initializerTree.Add(Token(SyntaxKind.CommaToken));
            }

            return InitializerExpression(
                SyntaxKind.CollectionInitializerExpression,
                SeparatedList<ExpressionSyntax>(
                    initializerTree.ToArray()
                ));
        }
    }
}