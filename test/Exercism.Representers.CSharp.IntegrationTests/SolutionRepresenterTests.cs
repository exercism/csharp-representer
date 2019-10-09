using Xunit;

namespace Exercism.Representers.CSharp.IntegrationTests
{
    public class SolutionRepresenterTests
    {
        [Theory]
        [TestSolutionsData]
        public void SolutionIsRepresentedCorrectly(TestSolution testSolution)
        {
            var representation = TestSolutionRepresenter.Run(testSolution);
            Assert.Equal(representation.Expected, representation.Actual);
        }
    }
}