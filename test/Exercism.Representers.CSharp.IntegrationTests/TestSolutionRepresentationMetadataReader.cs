using System.IO;
using System.Text.Json;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal record TestSolutionRepresentationMetadata(int ExpectedVersion, int ActualVersion);
    
    internal static class TestSolutionRepresentationMetadataReader
    {
        public static TestSolutionRepresentationMetadata Read(TestSolution solution) =>
            new(solution.ReadExpected(), solution.ReadActual());
        
        private static int ReadActual(this TestSolution solution) =>
            JsonDocument.Parse(solution.ReadFile("representation.json")).RootElement.GetProperty("version").GetInt32();

        private static int ReadExpected(this TestSolution solution) =>
            JsonDocument.Parse(solution.ReadFile("expected_representation.json")).RootElement.GetProperty("version").GetInt32();

        private static string ReadFile(this TestSolution solution, string fileName) =>
            File.ReadAllText(Path.Combine(solution.Directory, fileName));
    }
}