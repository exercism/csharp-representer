using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Exercism.Representers.CSharp.Simplification
{
    internal class RemoveComments : CSharpSyntaxRewriter
    {
        public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
        {
            if (trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
                trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                trivia.IsKind(SyntaxKind.MultiLineCommentTrivia) ||
                trivia.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia) ||
                trivia.IsKind(SyntaxKind.XmlComment))
                return default;

            return base.VisitTrivia(trivia);
        }
    }
}