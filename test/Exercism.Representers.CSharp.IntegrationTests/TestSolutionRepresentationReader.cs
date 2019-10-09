using System.IO;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionRepresentationReader
    {
        public static TestSolutionRepresentation Read(TestSolution solution) =>
            new TestSolutionRepresentation(solution.ReadExpected(), solution.ReadActual());

        private static string ReadActual(this TestSolution solution) =>
            solution.ReadFile("representation.txt");

        private static string ReadExpected(this TestSolution solution) =>
            solution.ReadFile("expected_representation.txt");

        private static string ReadFile(this TestSolution solution, string fileName) =>
            File.ReadAllText(Path.Combine(solution.Directory, fileName));
    }
}