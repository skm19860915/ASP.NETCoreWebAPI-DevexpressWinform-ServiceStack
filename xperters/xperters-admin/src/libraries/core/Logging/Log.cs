using System;
using System.Threading;

namespace Xperters.Core.Logging
{
    /// <summary>
    /// An optional static entry point for logging that can be easily referenced
    /// by different parts of an application. To configure the <see cref="T:Xperters.Core.Logging.Log" />
    /// set the Logger static property to a logger instance.
    /// </summary>
    /// <example>
    /// Log.Logger = new LoggerConfiguration()
    ///     .WithConsoleSink()
    ///     .CreateLogger();
    /// 
    /// var thing = "World";
    /// Logger.Information("Hello, {Thing}!", thing);
    /// </example>
    /// <remarks>
    /// The methods on <see cref="T:Xperters.Core.Logging.Log" /> (and its dynamic sibling <see cref="T:Xperters.Core.Logging.ILogger" />) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </remarks>
    public static class Log
    {
        private static ILogger _logger = new NullLogger();

        /// <summary>The globally-shared logger.</summary>
        /// <exception cref="T:System.ArgumentNullException"></exception>
        public static ILogger Logger
        {
            get { return _logger; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _logger = value;
            }
        }

        /// <summary>
        /// Resets <see cref="P:Xperters.Logging.Logger" /> to the default and disposes the original if possible
        /// </summary>
        public static void CloseAndFlush()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var disposable = Interlocked.Exchange(ref _logger, new NullLogger()) as IDisposable;
            if (disposable == null)
            {
                return;
            }

            disposable.Dispose();
        }

        /// <summary>
        /// Create a logger that enriches log events with the specified property.
        /// </summary>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return Logger.ForContext(propertyName, value, destructureObjects);
        }

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext<TSource>()
        {
            return Logger.ForContext<TSource>();
        }

        /// <summary>
        /// Create a logger that marks log events as being from the specified
        /// source type.
        /// </summary>
        /// <param name="source">Type generating log messages in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext(Type source)
        {
            return Logger.ForContext(source);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        public static void Write(LogEventLevel level, string messageTemplate)
        {
            Logger.Write(level, messageTemplate);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        public static void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            Logger.Write(level, messageTemplate, propertyValue);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        public static void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Write(level, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        public static void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Write(level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>Write a log event with the specified level.</summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        public static void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            Logger.Write(level, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        public static void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        public static void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        public static void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Logger.Write(level, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Determine if events at the specified level will be passed through
        /// to the log sinks.
        /// </summary>
        /// <param name="level">Level to check.</param>
        /// <returns>True if the level is enabled; otherwise, false.</returns>
        public static bool IsEnabled(LogEventLevel level)
        {
            return Logger.IsEnabled(level);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose(string messageTemplate)
        {
            Write(LogEventLevel.Verbose, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Verbose, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Verbose("Staring into space, wondering if we're alone.");
        /// </example>
        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Verbose" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Verbose(ex, "Staring into space, wondering where this comet came from.");
        /// </example>
        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug(string messageTemplate)
        {
            Write(LogEventLevel.Debug, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Debug, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Debug("Starting up at {StartedAt}.", DateTime.Now);
        /// </example>
        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>Debug(ex, "Swallowing a mundane exception.");</example>
        public static void Debug(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>Debug(ex, "Swallowing a mundane exception.");</example>
        public static void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>Debug(ex, "Swallowing a mundane exception.");</example>
        public static void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>Debug(ex, "Swallowing a mundane exception.");</example>
        public static void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Debug" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>Debug(ex, "Swallowing a mundane exception.");</example>
        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(string messageTemplate)
        {
            Write(LogEventLevel.Information, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Information, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Information, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Information, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Information" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
        /// </example>
        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(string messageTemplate)
        {
            Write(LogEventLevel.Warning, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Warning, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Warning("Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Warning" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
        /// </example>
        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(string messageTemplate)
        {
            Write(LogEventLevel.Error, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Error, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Error("Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>
        /// Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Error, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>
        /// Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Error" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
        /// </example>
        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>Fatal("Process terminating.");</example>
        public static void Fatal(string messageTemplate)
        {
            Write(LogEventLevel.Fatal, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>Fatal("Process terminating.");</example>
        public static void Fatal<T>(string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Fatal, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>Fatal("Process terminating.");</example>
        public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>Fatal("Process terminating.");</example>
        public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>Fatal("Process terminating.");</example>
        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <example>Fatal(ex, "Process terminating.");</example>
        public static void Fatal(Exception exception, string messageTemplate)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue">Object positionally formatted into the message template.</param>
        /// <example>Fatal(ex, "Process terminating.");</example>
        public static void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <example>Fatal(ex, "Process terminating.");</example>
        public static void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValue0">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue1">Object positionally formatted into the message template.</param>
        /// <param name="propertyValue2">Object positionally formatted into the message template.</param>
        /// <example>Fatal(ex, "Process terminating.");</example>
        public static void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        /// <summary>
        /// Write a log event with the <see cref="F:Xperters.Core.Logging.LogEventLevel.Fatal" /> level and associated exception.
        /// </summary>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>Fatal(ex, "Process terminating.");</example>
        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}
