using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using xperters.business.Interfaces;
using Xperters.Admin.ServiceModel;
using Xperters.Admin.ServiceModel.Exceptions;
using Xperters.Admin.ServiceModel.Withdrawals;

namespace Xperters.Admin.ServiceInterface.Services
{
    [Authenticate]
    public class WithdrawalsService : ServiceBase
    {
        private readonly IWithdrawalsManager _withdrawalsManager;
        private readonly ILogger<WithdrawalsService> _logger;

        public WithdrawalsService(IWithdrawalsManager withdrawalsManager, ILogger<WithdrawalsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _withdrawalsManager = withdrawalsManager;
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole)]
        public GetPaymentWithdrawalsResponse Get(GetPaymentWithdrawalsRequest request)
        {
            _logger.LogDebug("Get payment withdrawals for {@request}", request);

            var list = _withdrawalsManager.GetPaymentOutgoingForWithdrawals();

            if (list == null)
            {
                throw new XpertersException($"Error");
            }

            return new GetPaymentWithdrawalsResponse
            {
                PaymentOutgoingForWithdrawals = list
            };
        }
    }
}
