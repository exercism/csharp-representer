using System;
using System.Diagnostics;
using System.Linq;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionRepresenter
    {
        public static BulkSolutionRepresenterResults RunAll(Options options)
        {
            var bulkSolutions = BulkSolutionsReader.ReadAll(options);
            var bulkSolutionReresenterResults = bulkSolutions.Select(RunSingle).ToArray();
            return new BulkSolutionRepresenterResults(bulkSolutionReresenterResults);
        }

        private static BulkSolutionRepresenterResult RunSingle(BulkSolution solution)
        {
            var stopwatch = Stopwatch.StartNew();
            RunRepresenter(solution);
            stopwatch.Stop();

            return CreateBulkSolutionRepresenterResult(solution, stopwatch.Elapsed);
        }

        private static void RunRepresenter(BulkSolution solution) =>
            CSharp.Program.Main(new[] { solution.Slug, solution.Directory, solution.Directory });

        private static BulkSolutionRepresenterResult CreateBulkSolutionRepresenterResult(BulkSolution solution, TimeSpan elapsed) =>
            new BulkSolutionRepresenterResult(solution, BulkSolutionRepresentationReader.Read(solution), elapsed);
    }
}