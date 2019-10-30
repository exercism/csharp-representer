using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class BulkSolutionsReader
    {
        public static IEnumerable<BulkSolution> ReadAll(Options options) =>
            GetSolutionDirectories(options.Directory)
                .Select(solutionDirectory => CreateBulkSolution(options.Slug, solutionDirectory));

        private static IEnumerable<string> GetSolutionDirectories(string exerciseDirectory) =>
            Directory
                .EnumerateDirectories(exerciseDirectory, "*", SearchOption.AllDirectories)
                .Where(IsLeafDirectory)
                .Where(IsNotHidden)
                .OrderBy(directory => directory, StringComparer.Ordinal);

        private static bool IsLeafDirectory(string directory) =>
            !Directory.EnumerateDirectories(directory).Any();

        private static bool IsNotHidden(string directory) =>
            !directory.StartsWith(".");

        private static BulkSolution CreateBulkSolution(string slug, string solutionDirectory) =>
            new BulkSolution(slug, solutionDirectory);
    }
}