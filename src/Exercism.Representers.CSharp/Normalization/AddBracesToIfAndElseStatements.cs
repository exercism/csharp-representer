using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Normalization
{
    internal class AddBracesToIfAndElseStatements : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitIfStatement(IfStatementSyntax ifStatement)
        {
            if (ifStatement.Statement is BlockSyntax)
                return base.VisitIfStatement(ifStatement);

            return base.VisitIfStatement(
                ifStatement.WithStatement(
                    SyntaxFactory.Block(
                        SyntaxFactory.SingletonList(
                            ifStatement.Statement))));
        }

        public override SyntaxNode VisitElseClause(ElseClauseSyntax elseClause)
        {
            if (elseClause.Statement is BlockSyntax)
                return base.VisitElseClause(elseClause);

            return base.VisitElseClause(
                elseClause.WithStatement(
                    SyntaxFactory.Block(
                        SyntaxFactory.SingletonList(
                            elseClause.Statement))));
        }
    }
}