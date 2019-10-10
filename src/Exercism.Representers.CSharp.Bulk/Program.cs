using System.IO;
using CommandLine;

namespace Exercism.Representers.CSharp.Bulk
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Logging.Configure();

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Analyze);
        }

        private static void Analyze(Options options)
        {
            var bulkSolutionPresenterResults = BulkSolutionPresenter.RunAll(options);
            BulkSolutionPresenterResultsReport.Output(bulkSolutionPresenterResults);
        }
    }
}