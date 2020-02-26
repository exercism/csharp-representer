using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Normalization
{
    internal class SimplifyBooleanEquality : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            if (node.Left is LiteralExpressionSyntax leftLiteralExpression)
            {
                if (leftLiteralExpression.IsKind(SyntaxKind.TrueLiteralExpression))
                    return node.Right;

                if (leftLiteralExpression.IsKind(SyntaxKind.FalseLiteralExpression))
                    return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, node.Right);
            }
 
            if (node.Right is LiteralExpressionSyntax rightLiteralExpression)
            {
                if (rightLiteralExpression.IsKind(SyntaxKind.TrueLiteralExpression))
                    return node.Left;
                
                if (rightLiteralExpression.IsKind(SyntaxKind.FalseLiteralExpression))
                    return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, node.Left);
            }   
            
            return base.VisitBinaryExpression(node);
        }
    }
}