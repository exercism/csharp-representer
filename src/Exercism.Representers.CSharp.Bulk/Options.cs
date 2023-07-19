namespace Exercism.Representers.CSharp.Bulk
{
    internal class Options
    {
        public string Slug { get; }
        public string Directory { get; }

        public Options(string slug, string directory) =>
            (Slug, Directory) = (slug, directory);
    }
}