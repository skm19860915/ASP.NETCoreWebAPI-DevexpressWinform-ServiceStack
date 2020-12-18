using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace xperters.infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void LogAuthenticationRequests(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("AppBuilderExtensions");

            app.Use(async (context, next) =>
            {
                // Request method, scheme, and path
                logger.LogDebug("Request Method: {METHOD}", context.Request.Method);
                logger.LogDebug("Request Scheme: {SCHEME}", context.Request.Scheme);
                logger.LogDebug("Request Path: {PATH}", context.Request.Path);

                // Headers
                foreach (var header in context.Request.Headers)
                {
                    logger.LogDebug("Header: {KEY}: {VALUE}", header.Key, header.Value);
                }

                // Connection: RemoteIp
                logger.LogDebug("Request RemoteIp: {REMOTE_IP_ADDRESS}",
                    context.Connection.RemoteIpAddress);

                await next();
            });
        }

    }
}
