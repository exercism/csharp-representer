using Microsoft.CodeAnalysis;

namespace Exercism.Representers.CSharp
{
    internal class Solution
    {
        public string Name { get; }
        public string Slug { get; }
        public Document Document { get; }

        public Solution(string slug, string name, Document document) =>
            (Slug, Name, Document) = (slug, name, document);
    }
}