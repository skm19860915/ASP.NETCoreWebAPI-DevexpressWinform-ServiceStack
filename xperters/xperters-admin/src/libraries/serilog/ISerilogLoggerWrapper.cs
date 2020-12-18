using System.Collections.Generic;
using Serilog;
using Serilog.Core;

namespace Xperters.Serilog
{
    public interface ISerilogLoggerWrapper
    {
        Core.Logging.ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers);
    }
}
