using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp;

internal enum Concept
{
    ForLoops,
    ForeachLoops,
    IfStatements,
    SwitchStatements,
    SwitchExpressions,
    
    Recursion,
    
    GenericMethods,
    GenericTypes,
    ExtensionMethods
}

internal class IdentifyConcepts : CSharpSyntaxRewriter
{
    private readonly HashSet<Concept> _concepts;

    public IdentifyConcepts(HashSet<Concept> concepts) => _concepts = concepts;

    public override SyntaxNode VisitForStatement(ForStatementSyntax node)
    {
        _concepts.Add(Concept.ForLoops);
        return base.VisitForStatement(node);
    }

    public override SyntaxNode VisitForEachStatement(ForEachStatementSyntax node)
    {
        _concepts.Add(Concept.ForeachLoops);
        return base.VisitForEachStatement(node);
    }

    public override SyntaxNode VisitIfStatement(IfStatementSyntax node)
    {
        _concepts.Add(Concept.IfStatements);
        return base.VisitIfStatement(node);
    }

    public override SyntaxNode VisitSwitchStatement(SwitchStatementSyntax node)
    {
        _concepts.Add(Concept.SwitchStatements);
        return base.VisitSwitchStatement(node);
    }

    public override SyntaxNode VisitSwitchExpression(SwitchExpressionSyntax node)
    {
        _concepts.Add(Concept.SwitchExpressions);
        return base.VisitSwitchExpression(node);
    }

    public override SyntaxNode VisitParameter(ParameterSyntax node)
    {
        if (node.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.ThisKeyword)))
            _concepts.Add(Concept.ExtensionMethods);
        
        return base.VisitParameter(node);
    }

    public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        if (node.TypeParameterList != null)
            _concepts.Add(Concept.GenericMethods);

        return base.VisitMethodDeclaration(node);
    }

    public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        if (node.TypeParameterList != null)
            _concepts.Add(Concept.GenericTypes);
        
        return base.VisitClassDeclaration(node);
    }

    public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
    {
        if (node.TypeParameterList != null)
            _concepts.Add(Concept.GenericTypes);

        return base.VisitStructDeclaration(node);
    }

    public override SyntaxNode VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        if (node.TypeParameterList != null)
            _concepts.Add(Concept.GenericTypes);
        
        return base.VisitRecordDeclaration(node);
    }

    public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        if (node.Expression is IdentifierNameSyntax identifierName)
        {
            var parent = node.AncestorsAndSelf()
                .FirstOrDefault(node => node is MethodDeclarationSyntax or LocalFunctionStatementSyntax);
            
            if (parent is MethodDeclarationSyntax methodDeclaration && methodDeclaration.Identifier.Text == identifierName.Identifier.Text ||
                parent is LocalFunctionStatementSyntax localFunctionStatement && localFunctionStatement.Identifier.Text == identifierName.Identifier.Text)
                _concepts.Add(Concept.Recursion);
        }   

        return base.VisitInvocationExpression(node);
    }
}
