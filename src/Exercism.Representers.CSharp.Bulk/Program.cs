namespace Exercism.Representers.CSharp.Bulk;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = new Options(args[0], args[1]);
        Analyze(options);
    }

    private static void Analyze(Options options)
    {
        var bulkSolutionRepresenterResults = BulkSolutionRepresenter.RunAll(options);
        BulkSolutionRepresenterResultsReport.Output(bulkSolutionRepresenterResults);
    }
}