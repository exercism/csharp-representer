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

                var replacementNode
                    = BuildReplacementSyntaxWithCollectionInitialization(initializerExtractionResults.InitializerSyntaxNodePairs);


                return base.VisitInitializerExpression(replacementNode);
            }
            catch (InvalidKindException ike)
            {
                Log.Error(
                    $"{nameof(NormalizeDictionaryInitialization)}: {nameof(InitializerExpressionSyntax)} found with unexpected Kind {ike.Kind}");
                throw;
            }
            catch (Exception e)
            {
                Log.Error(e, $"{nameof(NormalizeDictionaryInitialization)}: unknown error");
                throw;
            }
        }

        private static bool IsDictionary(InitializerExpressionSyntax initializerExpression) =>
            initializerExpression?.Parent is ObjectCreationExpressionSyntax objectCreationExpression &&
            objectCreationExpression.Type is GenericNameSyntax genericName &&
            genericName.Identifier.Text == "Dictionary";

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodePairs)
            ExtractInitializersWithObjectSyntax(InitializerExpressionSyntax initializerExpression)
        {
            ArgumentSyntax GetArguemntSyntax(SyntaxNode node) =>
                node.DescendantNodes()
                    .OfType<ArgumentSyntax>()
                    .FirstOrDefault();

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

        private (bool Success, List<KeyValuePair<SyntaxNode, SyntaxNode>> InitializerSyntaxNodePairs)
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

        private InitializerExpressionSyntax BuildReplacementSyntaxWithCollectionInitialization(
            IEnumerable<KeyValuePair<SyntaxNode, SyntaxNode>> initializerSyntaxNodePairs)
        {
            var initializerTrees = new List<SyntaxNodeOrToken>();

            foreach (var pair in initializerSyntaxNodePairs)
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
                initializerTrees.Add(initializer);
                initializerTrees.Add(Token(SyntaxKind.CommaToken));
            }

            return InitializerExpression(
                SyntaxKind.CollectionInitializerExpression,
                SeparatedList<ExpressionSyntax>(
                    initializerTrees.ToArray()
                ));
        }
    }
}
