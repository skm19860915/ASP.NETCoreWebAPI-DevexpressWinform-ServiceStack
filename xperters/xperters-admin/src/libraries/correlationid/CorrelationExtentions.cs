using System;
using Microsoft.AspNetCore.Http;
using ServiceStack.Web;

namespace xperters.correlationid
{

    public static class CorrelationExtensions
    {
        
        public static readonly string CorrelationIdFieldName = "X-Correlation-ID";
        
        
        //Requests
        public static string GetCorrelationId(this IRequest request)
        {
            return request.Headers[CorrelationIdFieldName];
        }
        
        public static void SetCorrelationId(this IRequest request, string correlationId)
        {
            var currentId = request.GetCorrelationId();
            if (string.IsNullOrWhiteSpace(currentId))
            {
                if (request.OriginalRequest is HttpRequest originalRequest)
                    originalRequest.Headers[CorrelationIdFieldName] = correlationId;
                    
                request.Items[CorrelationIdFieldName] = correlationId;
            }
            else
            {
                throw new InvalidOperationException($"Request already has a correlationId of {currentId}");
            }
        }

        //Responses        
        public static string GetCorrelationId(this IResponse response)
        {
            return response.GetHeader(CorrelationIdFieldName);
        }
        
        public static void SetCorrelationId(this IResponse response, string correlationId)
        {
            var currentId = response.GetCorrelationId();
            if (string.IsNullOrWhiteSpace(currentId))
            {
                response.AddHeader(CorrelationIdFieldName,  correlationId);
                response.Items[CorrelationIdFieldName] = correlationId;
            }
            else
            {
                throw new InvalidOperationException($"Response already has a correlationId of {currentId}");
            }
        }
    }
}