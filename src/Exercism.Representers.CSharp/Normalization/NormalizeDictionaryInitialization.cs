using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Serilog;

namespace Exercism.Representers.CSharp.Normalization
{
    public class NormalizeDictionaryInitialization : CSharpSyntaxRewriter
    {
        private class InvalidKindException : Exception
        {
            public SyntaxKind Kind;
        }
        
        public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
        {
            SyntaxNode DefaultVisit() => base.VisitInitializerExpression(node);

            try
            {
                if (!IsDictionary(node))
                {
                    return DefaultVisit();
                }

                var initializerExtractionResults
                    = node.Kind() switch
                    {
                        SyntaxKind.CollectionInitializerExpression
                        => ExtractInitializersWithCollectionSyntax(node),
                        SyntaxKind.ObjectInitializerExpression
                        => ExtractInitializersWithObjectSyntax(node),
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

        private bool IsDictionary(InitializerExpressionSyntax initializerExpression)
        {
            var isDictionary = initializerExpression?.Parent?.ChildNodes()?.FirstOrDefault().GetText()?.ToString()
                ?.StartsWith("Dictionary");
            if (!isDictionary.HasValue)
            {
                Log.Error(
                    $"{nameof(NormalizeDictionaryInitialization)}: unable to retrieve the type information for {nameof(initializerExpression)}");
                return false;
            }

            if (!isDictionary.Value)
            {
                return false;
                // some other sort of object
            }

            return true;
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodes)
            ExtractInitializersWithObjectSyntax(InitializerExpressionSyntax initializerExpression)
        {
            ArgumentSyntax GetArguemntSyntax(SyntaxNode node)
            {
                if (!node.ChildNodes().Any())
                {
                    return null;
                }

                if (node.ChildNodes().First() is ArgumentSyntax)
                {
                    return node.ChildNodes().First() as ArgumentSyntax;
                }

                return GetArguemntSyntax(node.ChildNodes().First());
            }

            try
            {
                var initializerSyntaxNodes = initializerExpression.ChildNodes()
                    .Cast<AssignmentExpressionSyntax>()
                    .Select(aes => new KeyValuePair<SyntaxNode, SyntaxNode>(
                        GetArguemntSyntax(aes.Left).Expression, aes.Right)).ToList();

                return (true, initializerSyntaxNodes);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    $"{nameof(NormalizeDictionaryInitialization)}.{nameof(ExtractInitializersWithObjectSyntax)}: dictionary initialization found with unexpected syntax");
                return (false, default);
            }
        }

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodes)
            ExtractInitializersWithCollectionSyntax(InitializerExpressionSyntax initializerExpression)
        {
            try
            {
                var initializerSyntaxNodes = initializerExpression.ChildNodes()
                    .Select(n => new KeyValuePair<SyntaxNode, SyntaxNode>(
                        n.ChildNodes()?.FirstOrDefault(),
                        n.ChildNodes()?.LastOrDefault())).ToList();

                return (true, initializerSyntaxNodes);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    $"{nameof(NormalizeDictionaryInitialization)}.{nameof(ExtractInitializersWithCollectionSyntax)}: dictionary initialization found with unexpected syntax");
                return (false, default);
            }
        }

        private SyntaxNode BuildReplacementSyntaxWithCollectionInitialization(SyntaxKind initializationKind
            , IEnumerable<KeyValuePair<SyntaxNode, SyntaxNode>> visitedInitializerSyntaxNodes)
        {
            var initializerTree = new List<SyntaxNodeOrToken>();

            foreach (var pair in visitedInitializerSyntaxNodes)
            {
                var initializer = InitializerExpression(
                    SyntaxKind.ComplexElementInitializerExpression,
                    SeparatedList<ExpressionSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            pair.Key,
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