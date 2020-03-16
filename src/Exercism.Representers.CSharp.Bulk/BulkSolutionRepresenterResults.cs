using System;
using System.Linq;

namespace Exercism.Representers.CSharp.Bulk
{
    internal class BulkSolutionRepresenterResults
    {
        public BulkSolutionRepresenterResult[] Results { get; }
        public int UniqueSolutions { get; }
        public TimeSpan Elapsed { get; }

        public BulkSolutionRepresenterResults(BulkSolutionRepresenterResult[] results)
        {
            Results = results;
            Elapsed = results.Aggregate(TimeSpan.Zero, (total, result) => total + result.Elapsed);
            UniqueSolutions = results.Select(result => result.Representation.Representation).Distinct().Count();
        }

    }
}