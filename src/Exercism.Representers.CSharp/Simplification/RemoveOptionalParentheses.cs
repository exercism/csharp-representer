using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Simplification
{
    internal class RemoveOptionalParentheses : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
        {
            if (node.Parent is ConditionalExpressionSyntax)
                return base.Visit(node.Expression);

            return base.VisitParenthesizedExpression(node);
        }
    }
}