using System;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class NormalizationExtensions
    {
        public static string NormalizeNewlines(this string str) =>
            str.Replace("\n", Environment.NewLine);
    }
}