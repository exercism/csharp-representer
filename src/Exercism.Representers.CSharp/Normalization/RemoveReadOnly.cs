using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Normalization
{
    public class RemoveReadOnly : CSharpSyntaxRewriter
    {
        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            if (token.IsKind(SyntaxKind.ReadOnlyKeyword))
                return default;

            return base.VisitToken(token);
        }
    }
}
