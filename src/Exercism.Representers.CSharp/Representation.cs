namespace Exercism.Representers.CSharp
{
    internal class Representation
    {
        public string Original { get; }
        public string Simplified { get; }

        public Representation(string source, string simplified) =>
            (Original, Simplified) = (source, simplified);
    }
}