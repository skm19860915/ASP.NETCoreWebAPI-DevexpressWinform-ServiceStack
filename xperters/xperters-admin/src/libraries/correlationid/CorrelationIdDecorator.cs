using System;
using ServiceStack;
using ServiceStack.Web;

namespace xperters.correlationid
{
    public class CorrelationIdDecorator : IPlugin
    {

        private readonly Func<string> NextId;

        private readonly Action<IRequest, IResponse> RequestDecorator;
        
        private readonly Action<IRequest, IResponse, object> ResponseDecorator; 
        
        private static bool DoesNotHaveId(IRequest request) => string.IsNullOrWhiteSpace(request.GetCorrelationId());
        
        private static bool DoesNotHaveId(IResponse response) => string.IsNullOrWhiteSpace(response.GetCorrelationId());

        /// <summary>
        /// </summary>
        /// <param name="defaultGuidGenerator">Unless you explicitly want to control the correlation ID for testing just use the default generator.</param>
        public CorrelationIdDecorator(Func<string> defaultGuidGenerator = null)
        {
            NextId = defaultGuidGenerator ?? (() => Guid.NewGuid().ToString("D"));
            
            RequestDecorator = (request, response) => 
                               { 
                                   if (DoesNotHaveId(request))
                                   {
                                       request.SetCorrelationId(NextId());
                                   } 
                               };
            
            ResponseDecorator = (request, response, dto) =>
                                {
                                    if (DoesNotHaveId(response))
                                    {
                                        response.SetCorrelationId(request.GetCorrelationId());
                                    }
                                };
            
             
        }

        public void Register(IAppHost appHost)
        {
            appHost.PreRequestFilters.Add(RequestDecorator);
            appHost.GlobalResponseFilters.Add(ResponseDecorator);
        }
    }
}