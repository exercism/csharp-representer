using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    public class SolutionRepresenterTests
    {
        public SolutionRepresenterTests(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestOutput(output)
                .CreateLogger();
        }

        [Theory]
        [TestSolutionsData]
        public void SolutionIsRepresentedCorrectly(TestSolution solution)
        {
            var (representation, mapping, metadata) = TestSolutionRepresenter.Run(solution);
            Assert.Equal(representation.Expected, representation.Actual);
            Assert.Equal(mapping.Expected, mapping.Actual);
            Assert.Equal(metadata.ExpectedVersion, metadata.ActualVersion);
        }
    }
}
