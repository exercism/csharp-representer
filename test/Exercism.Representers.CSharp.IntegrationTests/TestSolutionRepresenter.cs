namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionRepresenter
    {
        public static TestSolutionRepresentation Run(TestSolution testSolution)
        {
            Program.Main(new[] { testSolution.Slug, testSolution.Directory });

            return CreateTestSolutionRepresentation(testSolution);
        }

        private static TestSolutionRepresentation CreateTestSolutionRepresentation(TestSolution solution) =>
            TestSolutionRepresentationReader.Read(solution);
    }
}