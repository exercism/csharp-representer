using Microsoft.CodeAnalysis;

namespace Exercism.Representers.CSharp;

internal record Solution(string Name, string Slug, Document Document);