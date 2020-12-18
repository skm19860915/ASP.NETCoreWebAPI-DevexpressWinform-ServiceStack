using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using xperters.business.Interfaces;
using Xperters.Admin.ServiceModel;
using Xperters.Admin.ServiceModel.Exceptions;
using Xperters.Admin.ServiceModel.Incoming;

namespace Xperters.Admin.ServiceInterface.Services
{
    [Authenticate]
    public class IncomingService : ServiceBase
    {
        private readonly IPaymentsIncomingManager _incomingManager;
        private readonly ILogger<IncomingService> _logger;

        public IncomingService(IPaymentsIncomingManager incomingManager, ILogger<IncomingService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _incomingManager = incomingManager;
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole)]
        public GetPaymentIncomingResponse Get(GetPaymentIncomingRequest request)
        {
            _logger.LogDebug("Get payment incoming for {@request}", request);

            var list = _incomingManager.GetPaymentIncoming();

            if (list == null)
            {
                throw new XpertersException($"Error");
            }

            return new GetPaymentIncomingResponse
            {
                PaymentIncoming = list
            };
        }
    }
}
