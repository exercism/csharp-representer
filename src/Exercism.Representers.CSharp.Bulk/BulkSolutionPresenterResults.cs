using System;
using System.Linq;

namespace Exercism.Representers.CSharp.Bulk
{
    internal class BulkSolutionPresenterResults
    {
        public BulkSolutionPresenterResult[] Results { get; }
        public int UniqueSolutions { get; }
        public TimeSpan Elapsed { get; }

        public BulkSolutionPresenterResults(BulkSolutionPresenterResult[] results)
        {
            Results = results;
            Elapsed = results.Aggregate(TimeSpan.Zero, (total, result) => total + result.Elapsed);
            UniqueSolutions = results.Select(result => result.Representation.Representation).Distinct().Count();
        }

    }
}