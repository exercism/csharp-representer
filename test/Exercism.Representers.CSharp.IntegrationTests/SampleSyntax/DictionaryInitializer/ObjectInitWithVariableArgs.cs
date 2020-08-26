using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class ObjectInitWithVariableArgs
    {
        public class Fake
        {
            public void Test()
            {
                var nn = 1;
                var ss = "one";
                var dict = new Dictionary<int, string>{ [nn] = ss};
            }
        }

        private SyntaxNode node = CompilationUnit()
            .WithUsings(
                SingletonList<UsingDirectiveSyntax>(
                    UsingDirective(
                        QualifiedName(
                            QualifiedName(
                                IdentifierName("System"),
                                IdentifierName("Collections")),
                            IdentifierName("Generic")))))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    ClassDeclaration("Fake")
                        .WithModifiers(
                            TokenList(
                                Token(SyntaxKind.PublicKeyword)))
                        .WithMembers(
                            SingletonList<MemberDeclarationSyntax>(
                                MethodDeclaration(
                                        PredefinedType(
                                            Token(SyntaxKind.VoidKeyword)),
                                        Identifier("Test"))
                                    .WithModifiers(
                                        TokenList(
                                            Token(SyntaxKind.PublicKeyword)))
                                    .WithBody(
                                        Block(
                                            LocalDeclarationStatement(
                                                VariableDeclaration(
                                                        IdentifierName("var"))
                                                    .WithVariables(
                                                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                            VariableDeclarator(
                                                                    Identifier("nn"))
                                                                .WithInitializer(
                                                                    EqualsValueClause(
                                                                        LiteralExpression(
                                                                            SyntaxKind.NumericLiteralExpression,
                                                                            Literal(1))))))),
                                            LocalDeclarationStatement(
                                                VariableDeclaration(
                                                        IdentifierName("var"))
                                                    .WithVariables(
                                                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                            VariableDeclarator(
                                                                    Identifier("ss"))
                                                                .WithInitializer(
                                                                    EqualsValueClause(
                                                                        LiteralExpression(
                                                                            SyntaxKind.StringLiteralExpression,
                                                                            Literal("one"))))))),
                                            LocalDeclarationStatement(
                                                VariableDeclaration(
                                                        IdentifierName("var"))
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
                                                                                                        Token(SyntaxKind
                                                                                                            .IntKeyword)),
                                                                                                    Token(SyntaxKind
                                                                                                        .CommaToken),
                                                                                                    PredefinedType(
                                                                                                        Token(SyntaxKind
                                                                                                            .StringKeyword))
                                                                                                }))))
                                                                            .WithInitializer(
                                                                                InitializerExpression(
                                                                                    SyntaxKind
                                                                                        .ObjectInitializerExpression,
                                                                                    SingletonSeparatedList<
                                                                                        ExpressionSyntax>(
                                                                                        AssignmentExpression(
                                                                                            SyntaxKind
                                                                                                .SimpleAssignmentExpression,
                                                                                            ImplicitElementAccess()
                                                                                                .WithArgumentList(
                                                                                                    BracketedArgumentList(
                                                                                                        SingletonSeparatedList
                                                                                                        <ArgumentSyntax
                                                                                                        >(
                                                                                                            Argument(
                                                                                                                IdentifierName(
                                                                                                                    "nn"))))),
                                                                                            IdentifierName(
                                                                                                "ss"))))))))))))))))
            .NormalizeWhitespace();
    }
}

/*
IObjectOrCollectionInitializationOperation (InitializerExpressionSyntax)
    ISimpleAssignmentOperation (AssignmentExpressionSyntax)
        IPropertyReferenceOperation (ImplicitElementAccessSyntax)
            IInstanceReferenceOperation (ImplicitElementAccessSyntax)
            IArgumentOperation (ArgumentSyntax)
                ILocalReferenceOperation (IdentifierNameSyntax)
        ILocalReferenceOperation (IdentifierNameSyntax)
*/

