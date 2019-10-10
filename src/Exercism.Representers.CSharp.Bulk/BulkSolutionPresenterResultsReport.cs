using System;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionPresenterResultsReport
    {
        public static void Output(BulkSolutionPresenterResults bulkSolutionPresenterResults)
        {
            Console.WriteLine($"# Solutions: {bulkSolutionPresenterResults.Results.Length}");
            Console.WriteLine($"# Unique solutions: {bulkSolutionPresenterResults.UniqueSolutions}");
            Console.WriteLine($"Time: {bulkSolutionPresenterResults.Elapsed}");
        }
    }
}