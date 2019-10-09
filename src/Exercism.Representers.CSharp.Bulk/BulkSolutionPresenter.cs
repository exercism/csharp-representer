using System;
using System.Diagnostics;
using System.Linq;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionPresenter
    {
        public static BulkSolutionPresenterResults RunAll(Options options)
        {
            var bulkSolutions = BulkSolutionsReader.ReadAll(options);
            var bulkSolutionPresenterResults = bulkSolutions.Select(RunSingle).ToArray();
            return new BulkSolutionPresenterResults(bulkSolutionPresenterResults);
        }

        private static BulkSolutionPresenterResult RunSingle(BulkSolution solution)
        {
            var stopwatch = Stopwatch.StartNew();
            RunPresenter(solution);
            stopwatch.Stop();

            return CreateBulkSolutionPresenterResult(solution, stopwatch.Elapsed);
        }

        private static void RunPresenter(BulkSolution solution) =>
            CSharp.Program.Main(new[] { solution.Slug, solution.Directory, solution.Directory });

        private static BulkSolutionPresenterResult CreateBulkSolutionPresenterResult(BulkSolution solution, TimeSpan elapsed) =>
            new BulkSolutionPresenterResult(solution, BulkSolutionRepresentationReader.Read(solution), elapsed);
    }
}