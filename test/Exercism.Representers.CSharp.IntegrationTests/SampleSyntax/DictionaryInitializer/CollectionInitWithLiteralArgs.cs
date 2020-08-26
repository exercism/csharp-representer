using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class CollectionInitWithLiteralArgs
    {
        Dictionary<int, string> dict = new Dictionary<int, string> {{1, "one"}, {2, "two"}};

        private SyntaxNode node = CompilationUnit()
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    FieldDeclaration(
                        VariableDeclaration(
                                GenericName(
                                        Identifier("Dictionary"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SeparatedList<TypeSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    PredefinedType(
                                                        Token(SyntaxKind.IntKeyword)),
                                                    Token(SyntaxKind.CommaToken),
                                                    PredefinedType(
                                                        Token(SyntaxKind.StringKeyword))
                                                }))))
                            .WithVariables(
                                SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    VariableDeclarator(
                                            Identifier("dict"))
                                        .WithInitializer(
                                            EqualsValueClause(
                                                ObjectCreationExpression(
                                                        GenericName(
                                                                Identifier("Dictionary"))
                                                            .WithTypeArgumentList(
                                                                TypeArgumentList(
                                                                    SeparatedList<TypeSyntax>(
                                                                        new SyntaxNodeOrToken[]
                                                                        {
                                                                            PredefinedType(
                                                                                Token(SyntaxKind.IntKeyword)),
                                                                            Token(SyntaxKind.CommaToken),
                                                                            PredefinedType(
                                                                                Token(SyntaxKind.StringKeyword))
                                                                        }))))
                                                    .WithInitializer(
                                                        InitializerExpression(
                                                            SyntaxKind.CollectionInitializerExpression,
                                                            SeparatedList<ExpressionSyntax>(
                                                                new SyntaxNodeOrToken[]
                                                                {
                                                                    InitializerExpression(
                                                                        SyntaxKind.ComplexElementInitializerExpression,
                                                                        SeparatedList<ExpressionSyntax>(
                                                                            new SyntaxNodeOrToken[]
                                                                            {
                                                                                LiteralExpression(
                                                                                    SyntaxKind.NumericLiteralExpression,
                                                                                    Literal(1)),
                                                                                Token(SyntaxKind.CommaToken),
                                                                                LiteralExpression(
                                                                                    SyntaxKind.StringLiteralExpression,
                                                                                    Literal("one"))
                                                                            })),
                                                                    Token(SyntaxKind.CommaToken),
                                                                    InitializerExpression(
                                                                        SyntaxKind.ComplexElementInitializerExpression,
                                                                        SeparatedList<ExpressionSyntax>(
                                                                            new SyntaxNodeOrToken[]
                                                                            {
                                                                                LiteralExpression(
                                                                                    SyntaxKind.NumericLiteralExpression,
                                                                                    Literal(2)),
                                                                                Token(SyntaxKind.CommaToken),
                                                                                LiteralExpression(
                                                                                    SyntaxKind.StringLiteralExpression,
                                                                                    Literal("two"))
                                                                            }))
                                                                }))))))))))
            .NormalizeWhitespace();
    }
}