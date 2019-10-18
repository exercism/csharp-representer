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
            ClearPlaceholders();

            return base.VisitMethodDeclaration(node);
        }
        
        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            if (AddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitParameter(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitParameter(node);
        }

        public override SyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (AddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitVariableDeclarator(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitVariableDeclarator(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (TryGetPlaceholder(node.Identifier, out var placeholder))
                return base.VisitIdentifierName(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitIdentifierName(node);
        }

        private static void ClearPlaceholders() =>
            IdentifierToPlaceholder.Clear();

        private static bool AddPlaceholder(SyntaxToken identifier, out string placeholder)
        {
            if (IdentifierToPlaceholder.TryGetValue(identifier.ValueText, out placeholder))
                return true;
            
            placeholder = $"PLACEHOLDER_{IdentifierToPlaceholder.Count + 1}";
            return IdentifierToPlaceholder.TryAdd(identifier.ValueText, placeholder);
        }

        private static bool TryGetPlaceholder(SyntaxToken identifier, out string placeholder) =>
            IdentifierToPlaceholder.TryGetValue(identifier.ValueText, out placeholder);
    }
}