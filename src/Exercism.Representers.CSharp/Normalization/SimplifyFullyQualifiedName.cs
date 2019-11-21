using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Normalization
{
    internal class SimplifyFullyQualifiedName : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            if (node.Expression.IdentifierName() == "System")
                return base.Visit(node.Name);

            return base.VisitMemberAccessExpression(node);
        }

        public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
        {
            if (node.Left.IdentifierName() == "System")
                return base.Visit(node.Right);

            return base.VisitQualifiedName(node);
        }
    }
}