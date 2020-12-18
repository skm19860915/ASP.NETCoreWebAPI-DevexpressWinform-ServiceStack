using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Core;
using Xperters.Core.Logging;
using ILogger = Xperters.Core.Logging.ILogger;
using SerilogLogEventLevel = Serilog.Events.LogEventLevel;

namespace Xperters.Serilog
{
    public class SerilogILoggerWrapper : ISerilogLoggerWrapper, ILogger, IDisposable
    {
        private global::Serilog.ILogger _logger;
        private readonly object _syncLock = new object();

        public SerilogILoggerWrapper(global::Serilog.ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException("logger");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return new SerilogILoggerWrapper(_logger.ForContext(propertyName, value, destructureObjects));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILogger ForContext<TSource>()
        {
            return new SerilogILoggerWrapper(_logger.ForContext<TSource>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILogger ForContext(Type source)
        {
            return new SerilogILoggerWrapper(_logger.ForContext(source));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        { 
            return new SerilogILoggerWrapper(_logger.ForContext(enrichers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(LogEventLevel level, string messageTemplate)
        {
            _logger.Write((SerilogLogEventLevel) level, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            _logger.Write((SerilogLogEventLevel) level, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Write((SerilogLogEventLevel) level, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Write((SerilogLogEventLevel) level, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            _logger.Write((SerilogLogEventLevel) level, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            _logger.Write((SerilogLogEventLevel) level, exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Write((SerilogLogEventLevel) level, exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            _logger.Write((SerilogLogEventLevel) level, exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Write((SerilogLogEventLevel) level, exception, messageTemplate, propertyValue0, propertyValue1,
                propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Write((SerilogLogEventLevel) level, exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEnabled(LogEventLevel level)
        {
            return _logger.IsEnabled((SerilogLogEventLevel) level);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose(string messageTemplate)
        {
            _logger.Verbose(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            _logger.Verbose(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Verbose(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Verbose(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            _logger.Verbose(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose(Exception exception, string messageTemplate)
        {
            _logger.Verbose(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Verbose(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Verbose(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Verbose(exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string messageTemplate)
        {
            _logger.Debug(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            _logger.Debug(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Debug(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Debug(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(Exception exception, string messageTemplate)
        {
            _logger.Debug(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Debug(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Debug(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Debug(exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information(string messageTemplate)
        {
            _logger.Information(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T>(string messageTemplate, T propertyValue)
        {
            _logger.Information(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Information(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information(string messageTemplate, params object[] propertyValues)
        {
            _logger.Information(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information(Exception exception, string messageTemplate)
        {
            _logger.Information(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Information(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Information(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Information(exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string messageTemplate)
        {
            _logger.Warning(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            _logger.Warning(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Warning(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Warning(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            _logger.Warning(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(Exception exception, string messageTemplate)
        {
            _logger.Warning(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Warning(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Warning(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Warning(exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string messageTemplate)
        {
            _logger.Error(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T>(string messageTemplate, T propertyValue)
        {
            _logger.Error(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Error(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Error(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(Exception exception, string messageTemplate)
        {
            _logger.Error(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Error(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Error(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Error(exception, messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal(string messageTemplate)
        {
            _logger.Fatal(messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            _logger.Fatal(messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Fatal(messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            _logger.Fatal(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            _logger.Fatal(messageTemplate, propertyValues);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal(Exception exception, string messageTemplate)
        {
            _logger.Fatal(exception, messageTemplate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            _logger.Fatal(exception, messageTemplate, propertyValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            _logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            _logger.Fatal(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            _logger.Fatal(exception, messageTemplate, propertyValues);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            lock (_syncLock)
            {
                // ReSharper disable once SuspiciousTypeConversion.Global
                var disposable = _logger as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                _logger = new LoggerConfiguration()
                    .CreateLogger();
            }
        }
    }
}
