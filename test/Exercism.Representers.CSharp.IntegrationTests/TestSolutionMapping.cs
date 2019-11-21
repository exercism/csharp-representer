using System.Collections.Generic;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal class TestSolutionMapping
    {
        public Dictionary<string, string> Expected { get; }
        public Dictionary<string, string> Actual { get; }

        public TestSolutionMapping(Dictionary<string, string> expected, Dictionary<string, string> actual) =>
            (Expected, Actual) = (expected, actual);
    }
}