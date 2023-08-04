using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Exercism.Representers.CSharp.Normalization;

internal class RemoveUsingDirectives : CSharpSyntaxRewriter
{
    public override SyntaxNode VisitUsingDirective(UsingDirectiveSyntax node) => null;
}