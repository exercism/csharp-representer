using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionsReader
    {
        public static IEnumerable<TestSolution> ReadAll() =>
            GetTestSolutionGroupDirectories().SelectMany(GetTestSolutionDirectories).Select(CreateTestSolution);

        private static IEnumerable<string> GetTestSolutionGroupDirectories() =>
            Directory.GetDirectories("Solutions");

        private static IEnumerable<string> GetTestSolutionDirectories(string solutionsGroupDirectory) =>
            Directory.GetDirectories(solutionsGroupDirectory);

        private static TestSolution CreateTestSolution(string solutionDirectory) =>
            new TestSolution("Fake", Path.GetFullPath(solutionDirectory));
    }
}