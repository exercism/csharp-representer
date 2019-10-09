using Serilog;

namespace Exercism.Representers.CSharp.Bulk
{
    internal static class Logging
    {
        public static void Configure() =>
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
    }
}