using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Serilog.Core.Enrichers;
using ServiceStack;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using ServiceStack.Web;
using xperters.correlationid;

namespace Xperters.Admin.Api
{
	public class LogRequestLogger : IRequestLogger
	{
		private ILog Logger => LogManager.GetLogger(GetType());

		public bool EnableSessionTracking { get; set; }
		public bool EnableRequestBodyTracking { get; set; }
		public bool EnableResponseTracking { get; set; }
		public bool EnableErrorTracking { get; set; }
		public bool LimitToServiceRequests { get; set; }
		public string[] RequiredRoles { get; set; }
		public Func<IRequest, bool> SkipLogging { get; set; }
		public Type[] ExcludeRequestDtoTypes { get; set; }
		public Type[] HideRequestBodyForRequestDtoTypes { get; set; }
		public Action<IRequest, RequestLogEntry> RequestLogFilter { get; set; }
        public Func<object, bool> IgnoreFilter { get; set; }
        public Func<DateTime> CurrentDateFn { get; set; } = () => DateTime.UtcNow;

		public List<RequestLogEntry> GetLatestLogs(int? take)
		{
			return new List<RequestLogEntry>();
		}

		public void Log(IRequest request, object requestDto, object response, TimeSpan elapsed)
		{
			if (ShouldSkip(request, requestDto))
				return;

			if (!Guid.TryParse(request.Headers[CorrelationExtensions.CorrelationIdFieldName], out var correlationId))
			{
				correlationId = Guid.Empty;
			}

			var requestType = requestDto?.GetType();

			var originalRequest = (HttpRequest)request.OriginalRequest;

			var old = originalRequest.Headers["Authorization"];
			originalRequest.Headers["Authorization"] = "";

			// var context = Logger.ForContext(new[]
			// {
			// 	new PropertyEnricher("requestDto", request.ToSafeJson()),
			// 	new PropertyEnricher("responseDto", response.ToSafeJson())
			// });
			// context.Debug($"Request : {requestType?.Name} id:[{correlationId}] took {elapsed.TotalMilliseconds}ms.");
			// originalRequest.Headers["Authorization"] = old;
		}

		private bool ShouldSkip(IRequest req, object requestDto)
		{
			if (SkipLogging?.Invoke(req) == true) return true;

			if (req.IsLocal) return true;
			var dto = requestDto ?? req.Dto;

			if (LimitToServiceRequests && (dto == null || dto.GetType().Name.Contains("OpenApiSpecification")))
				return true;

			var requestType = dto?.GetType();

			return ExcludeRequestDtoTypes != null
					&& requestType != null
					&& ExcludeRequestDtoTypes.Contains(requestType);
		}
	}
}