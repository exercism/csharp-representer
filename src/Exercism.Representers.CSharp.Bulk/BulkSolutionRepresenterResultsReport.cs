using System;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionRepresenterResultsReport
    {
        public static void Output(BulkSolutionRepresenterResults bulkSolutionRepresenterResults)
        {
            Console.WriteLine($"# Solutions: {bulkSolutionRepresenterResults.Results.Length}");
            Console.WriteLine($"# Unique solutions: {bulkSolutionRepresenterResults.UniqueSolutions}");
            Console.WriteLine($"Time: {bulkSolutionRepresenterResults.Elapsed}");
        }
    }
}