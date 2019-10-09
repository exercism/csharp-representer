namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal class TestSolutionRepresentation
    {
        public string Expected { get; }
        public string Actual { get; }

        public TestSolutionRepresentation(string expected, string actual) =>
            (Expected, Actual) = (expected, actual);
    }
}