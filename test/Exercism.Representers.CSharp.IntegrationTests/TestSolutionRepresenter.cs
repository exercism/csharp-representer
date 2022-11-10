namespace Exercism.Representers.CSharp.IntegrationTests
{
    internal static class TestSolutionRepresenter
    {
        public static (TestSolutionRepresentationText representation, TestSolutionMapping mapping, TestSolutionRepresentationMetadata metadata) Run(TestSolution solution)
        {
            RunRepresenter(solution);

            var representation = TestSolutionRepresentationTextReader.Read(solution);
            var mapping = TestSolutionMappingReader.Read(solution);
            var metadata = TestSolutionRepresentationMetadataReader.Read(solution);
            return (representation, mapping, metadata);
        }

        private static void RunRepresenter(TestSolution solution) =>
            Program.Main(new[] { solution.Slug, solution.Directory, solution.Directory, 
                "--SkipLogConfiguration" });
    }
}