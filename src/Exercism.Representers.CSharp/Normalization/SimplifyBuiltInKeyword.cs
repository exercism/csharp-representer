using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.Normalization
{
    internal class SimplifyBuiltInKeyword : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (node.Expression.IdentifierName() == "String")
                return base.Visit(
                    node.WithExpression(PredefinedType(Token(SyntaxKind.StringKeyword))
                        .WithTriviaFrom(node.Expression)));

            return base.VisitMemberAccessExpression(node);
        }

        public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            if (node.Type.IdentifierName() == "String")
                return base.Visit(
                    node.WithType(PredefinedType(Token(SyntaxKind.StringKeyword))
                        .WithTriviaFrom(node.Type)));

            return base.VisitVariableDeclaration(node);
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            if (node.Left.IdentifierName() == "String")
                return base.Visit(
                    node.WithLeft(IdentifierName("string")
                        .WithTriviaFrom(node.Left)));

            return base.VisitQualifiedName(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.ReturnType.IdentifierName() == "String")
                return base.VisitMethodDeclaration(
                    node.WithReturnType(IdentifierName("string")
                        .WithTriviaFrom(node.ReturnType)));

            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            if (node.Type.IdentifierName() == "String")
                return base.VisitParameter(
                    node.WithType(IdentifierName("string")
                        .WithTriviaFrom(node.Type)));

            return base.VisitParameter(node);
        }
    }
}