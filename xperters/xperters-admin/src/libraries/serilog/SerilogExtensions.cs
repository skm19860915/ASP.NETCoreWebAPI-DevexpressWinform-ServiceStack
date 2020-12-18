// ReSharper disable once CheckNamespace

using Xperters.Core.Logging;
using Xperters.Serilog;

namespace Xperters
{
    public static class SerilogExtensions
    {
        public static ILogger ToXpertersILogger(this global::Serilog.ILogger logger)
        {
            return new SerilogILoggerWrapper(logger);
        }
    }
}