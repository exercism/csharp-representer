using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionMappingReader
    {
        public static TestSolutionMapping Read(TestSolution solution) =>
            new TestSolutionMapping(solution.ReadExpected(), solution.ReadActual());

        private static Dictionary<string, string> ReadActual(this TestSolution solution) =>
            solution.ReadMapping("mapping.json");

        private static Dictionary<string, string> ReadExpected(this TestSolution solution) =>
            solution.ReadMapping("expected_mapping.json");

        private static Dictionary<string, string> ReadMapping(this TestSolution solution, string fileName) =>
            JsonSerializer.Deserialize<Dictionary<string, string>>(solution.ReadFile(fileName));
        
        private static string ReadFile(this TestSolution solution, string fileName) =>
            File.ReadAllText(Path.Combine(solution.Directory, fileName));
    }
}