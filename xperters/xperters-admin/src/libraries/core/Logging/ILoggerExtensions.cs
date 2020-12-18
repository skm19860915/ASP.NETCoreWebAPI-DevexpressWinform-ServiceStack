using System.Runtime.CompilerServices;

namespace Xperters.Core.Logging
{
    // ReSharper disable once InconsistentNaming
    public static class ILoggerExtensions
    {
        public static ILogger ForHere(this ILogger logger,
                [CallerFilePath] string sourceFile = null,
                [CallerLineNumber] int sourceLine = 0)
        {
            return logger
                .ForContext("SourceFile", sourceFile)
                .ForContext("SourceLine", sourceLine);
        }
    }
}
