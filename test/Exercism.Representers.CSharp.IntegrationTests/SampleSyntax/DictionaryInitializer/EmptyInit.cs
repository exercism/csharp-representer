using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class EmptyInit
    {
        Dictionary<int, string> dict = new Dictionary<int, string>{};
        SyntaxNode node = CompilationUnit()
            .WithMembers(
            SingletonList<MemberDeclarationSyntax>(
        FieldDeclaration(
        VariableDeclaration(
        GenericName(
        Identifier("Dictionary"))
        .WithTypeArgumentList(
            TypeArgumentList(
        SeparatedList<TypeSyntax>(
        new SyntaxNodeOrToken[]{
            PredefinedType(
                Token(SyntaxKind.IntKeyword)),
            Token(SyntaxKind.CommaToken),
            PredefinedType(
                Token(SyntaxKind.StringKeyword))}))))
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
        new SyntaxNodeOrToken[]{
            PredefinedType(
                Token(SyntaxKind.IntKeyword)),
            Token(SyntaxKind.CommaToken),
            PredefinedType(
                Token(SyntaxKind.StringKeyword))}))))
        .WithInitializer(
                InitializerExpression(
                    SyntaxKind.ObjectInitializerExpression)))))))))
            .NormalizeWhitespace();

    }
}