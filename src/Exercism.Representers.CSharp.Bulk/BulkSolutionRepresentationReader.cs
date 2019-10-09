using System.IO;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionRepresentationReader
    {
        public static BulkSolutionRepresentation Read(BulkSolution solution) =>
            new BulkSolutionRepresentation(solution.ReadRepresentation());

        private static string ReadRepresentation(this BulkSolution solution) =>
            File.ReadAllText(Path.Combine(solution.Directory, "representation.txt"));
    }
}