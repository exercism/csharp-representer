namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionRepresenter
    {
        public static (TestSolutionRepresentation representation, TestSolutionMapping mapping) Run(TestSolution solution)
        {
            RunRepresenter(solution);

            var representation = TestSolutionRepresentationReader.Read(solution);
            var mapping = TestSolutionMappingReader.Read(solution);
            return (representation, mapping);
        }

        private static void RunRepresenter(TestSolution solution) =>
            Program.Main(new[] { solution.Slug, solution.Directory, solution.Directory });
    }
}