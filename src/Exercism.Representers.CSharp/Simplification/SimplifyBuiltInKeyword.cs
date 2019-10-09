using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.Simplification
{
    internal class SimplifyBuiltInKeyword : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (node.Expression.IsEquivalentWhenNormalized(IdentifierName("String")))
                return base.Visit(
                    node.WithExpression(
                        PredefinedType(
                            Token(SyntaxKind.StringKeyword))
                        .WithTriviaFrom(node.Expression)));

            return base.VisitMemberAccessExpression(node);
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            if (node.Type.IsEquivalentWhenNormalized(IdentifierName("String")))
                return base.Visit(
                    node.WithType(
                        PredefinedType(
                            Token(SyntaxKind.StringKeyword))
                        .WithTriviaFrom(node.Type)));

            return base.VisitVariableDeclaration(node);
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            if (node.Left.IsEquivalentWhenNormalized(IdentifierName("String")))
                return base.Visit(
                    node.WithLeft(
                        IdentifierName("string").WithTriviaFrom(node.Left)));

            return base.VisitQualifiedName(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax methodDeclaration)
        {
            if (methodDeclaration.ReturnType.IsEquivalentWhenNormalized(IdentifierName("String")))
                return base.VisitMethodDeclaration(methodDeclaration.WithReturnType(IdentifierName("string").WithTriviaFrom(methodDeclaration.ReturnType)));

            return base.VisitMethodDeclaration(methodDeclaration);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax parameter)
        {
            if (parameter.Type.IsEquivalentWhenNormalized(IdentifierName("String")))
                return base.VisitParameter(parameter.WithType(IdentifierName("string").WithTriviaFrom(parameter.Type)));

            return base.VisitParameter(parameter);
        }
    }
}