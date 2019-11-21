using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Normalization
{   
    internal class NormalizeIdentifiers : CSharpSyntaxRewriter
    {
        private readonly Dictionary<string, string> _identifierToPlaceholder = new Dictionary<string, string>();

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (TryAddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitClassDeclaration(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));

            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (TryAddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitMethodDeclaration(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));

            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitParameter(ParameterSyntax node)
        {
            if (TryAddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitParameter(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitParameter(node);
        }

        public override SyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (TryAddPlaceholder(node.Identifier, out var placeholder))
                return base.VisitVariableDeclarator(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitVariableDeclarator(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (TryGetPlaceholder(node.Identifier, out var placeholder))
                return base.VisitIdentifierName(node.WithIdentifier(SyntaxFactory.Identifier(placeholder)));
            
            return base.VisitIdentifierName(node);
        }

        private bool TryAddPlaceholder(SyntaxToken identifier, out string placeholder)
        {
            if (_identifierToPlaceholder.TryGetValue(identifier.ValueText, out placeholder))
                return true;
            
            placeholder = $"PLACEHOLDER_{_identifierToPlaceholder.Count + 1}";
            return _identifierToPlaceholder.TryAdd(identifier.ValueText, placeholder);
        }

        private bool TryGetPlaceholder(SyntaxToken identifier, out string placeholder) =>
            _identifierToPlaceholder.TryGetValue(identifier.ValueText, out placeholder);
    }
}