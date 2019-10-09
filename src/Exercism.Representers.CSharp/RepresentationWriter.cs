using System.IO;

namespace Exercism.Representers.CSharp
{
    internal static class RepresentationWriter
    {
        public static void WriteToFile(Options options, Representation representation) =>
            File.WriteAllText(GetRepresentationFilePath(options), representation.Simplified);

        private static string GetRepresentationFilePath(Options options) =>
            Path.GetFullPath(Path.Combine(options.Directory, "representation.txt"));
    }
}