using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Web;

namespace Xperters.Admin.ServiceInterface.Extensions
{
	public static class LoggerExtensions
	{

		public static void LogDebug(this ILogger logger, IRequest request, [CallerLineNumber] int callerLineNumber = default(int), [CallerFilePath] string callerFilePath = null)
		{
			if (logger == null) return;

			var enrichedLog = GetEnrichedLogMessage(request, callerLineNumber, callerFilePath, null, null);
			logger.LogDebug(enrichedLog.Template, enrichedLog.Args.ToArray());
		}

		public static void LogDebug(this ILogger logger, IRequest request, string message, [CallerLineNumber] int callerLineNumber = default(int), [CallerFilePath] string callerFilePath = null)
		{
			if (logger == null) return;

			var enrichedLog = GetEnrichedLogMessage(request, callerLineNumber, callerFilePath, message, null);
			logger.LogDebug(enrichedLog.Template, enrichedLog.Args.ToArray());
		}

		public static void LogError(this ILogger logger, IRequest request, Exception exception, string messageTemplate, object messageTemplateValue1, [CallerLineNumber] int callerLineNumber = default(int), [CallerFilePath] string callerFilePath = null)
		{
			if (logger == null) return;

			var enrichedLog = GetEnrichedLogMessage(request, callerLineNumber, callerFilePath, messageTemplate, messageTemplateValue1);
			logger.LogError(exception, enrichedLog.Template, enrichedLog.Args.ToArray());
		}

		private static EnrichedLogMessage GetEnrichedLogMessage(IRequest request,
			int callerLineNumber,
			string callerFilePath,
			string message,
			params object[] args)
		{
			var templateValues = new Dictionary<string, object> { { "Date", DateTime.UtcNow } };
			templateValues.Add("CallerLineNumber", callerLineNumber);
			templateValues.Add("CallerFilePath", callerFilePath);

			if (request != null)
			{
				var userSession = request.SessionAs<AuthUserSession>();
				templateValues.Add("CorrelationId", request.GetCorrelationId());
				templateValues.Add("RequestType", request.Dto?.GetType().Name);
				templateValues.Add("UserDisplayName", userSession?.DisplayName);
				templateValues.Add("UserEmail", userSession?.Email);
				templateValues.Add("UserId", userSession?.Id);
				templateValues.Add("Url", request.RawUrl);
			}

			var template = string.Join(", ", templateValues.Select(o => $"{o.Key}: {{{o.Key}}}"));
			if (string.IsNullOrWhiteSpace(message))
				return new EnrichedLogMessage(template, templateValues.Select(o => o.Value).ToList());

			// append message and its args to the enriched log message
			var values = templateValues.Select(o => o.Value).ToList();
			values.AddRange(args.ToListOrEmptyIfNull());
			return new EnrichedLogMessage($"{template}, Message : {message}", values);
		}

		private sealed class EnrichedLogMessage
		{
			public EnrichedLogMessage(string log, List<object> args)
			{
				Template = log;
				Args = args;
			}

			public string Template { get; }
			public List<object> Args { get; }
		}
	}
}
