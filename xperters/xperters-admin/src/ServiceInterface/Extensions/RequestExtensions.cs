using System;
using System.Collections.Generic;
using System.Linq;

namespace Xperters.Admin.ServiceInterface.Extensions
{
	public static class RequestExtensions
	{
		public static string GetCorrelationId(this ServiceStack.Web.IRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var values = request.Headers?.GetValues("X-Correlation-ID");
			return string.Join("|", values.ToListOrEmptyIfNull());
		}

		public static List<T> ToListOrEmptyIfNull<T>(this IEnumerable<T> collection)
		{
			return collection?.ToList() ?? Enumerable.Empty<T>().ToList();
		}
	}
}
