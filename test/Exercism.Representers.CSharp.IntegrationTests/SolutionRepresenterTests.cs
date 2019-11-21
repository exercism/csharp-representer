using Xunit;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    public class SolutionRepresenterTests
    {
        [Theory]
        [TestSolutionsData]
        public void SolutionIsRepresentedCorrectly(TestSolution solution)
        {
            var (representation, mapping) = TestSolutionRepresenter.Run(solution);
            Assert.Equal(representation.Expected, representation.Actual);
            Assert.Equal(mapping.Expected, mapping.Actual);
        }
    }
}