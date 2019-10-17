using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Simplification
{   
    internal class NormalizeIdentifiers : CSharpSyntaxRewriter
    {
        private static readonly Dictionary<string, string> IdentifierToPlaceholder = new Dictionary<string, string>();
        
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {   
            IdentifierToPlaceholder.Clear();

            foreach (var parameter in node.ParameterList.Parameters)
                IdentifierToPlaceholder[parameter.Identifier.ValueText] = $"PLACEHOLDER_{IdentifierToPlaceholder.Count + 1}";

            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            foreach (var parameter in node.ParameterList.Parameters)
                IdentifierToPlaceholder[parameter.Identifier.ValueText] = $"PLACEHOLDER_{IdentifierToPlaceholder.Count + 1}";
            
            return base.VisitLocalFunctionStatement(node);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            if (IdentifierToPlaceholder.TryGetValue(node.Identifier.ValueText, out var placeholder))
                return base.VisitParameter(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitParameter(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (IdentifierToPlaceholder.TryGetValue(node.Identifier.ValueText, out var placeholder) &&
                !(node.Parent is MemberAccessExpressionSyntax))
                return base.VisitIdentifierName(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitIdentifierName(node);
        }
    }
}