using System;

namespace Exercism.Representers.CSharp.Bulk
{
    internal class BulkSolutionRepresenterResult
    {
        public BulkSolution Solution { get; }
        public BulkSolutionRepresentation Representation { get; }
        public TimeSpan Elapsed { get; }

        public BulkSolutionRepresenterResult(BulkSolution solution, BulkSolutionRepresentation representation, TimeSpan elapsed) =>
            (Solution, Representation, Elapsed) = (solution, representation, elapsed);
    }
}