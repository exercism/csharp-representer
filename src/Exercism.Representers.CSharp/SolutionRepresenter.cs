using Exercism.Representers.CSharp.Simplification;
using Microsoft.CodeAnalysis;

namespace Exercism.Representers.CSharp
{
    internal static class SolutionRepresenter
    {
        public static Representation Represent(Solution solution) =>
            new Representation(solution.GetOriginal(), solution.GetSimplified());

        private static string GetOriginal(this Solution solution) =>
            solution.Document.GetText();

        private static string GetSimplified(this Solution solution) =>
            solution.Document.Simplify().GetText();

        private static string GetText(this TextDocument document) =>
            document.GetTextAsync().GetAwaiter().GetResult().ToString();
    }
}