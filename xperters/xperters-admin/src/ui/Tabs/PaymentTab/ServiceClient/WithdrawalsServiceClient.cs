using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Withdrawals;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.PaymentTab.ServiceClient
{
    public class WithdrawalsServiceClient : IWithdrawalsServiceClient
    {
        private readonly IXpertersAdminServiceClient _serviceClient;
        public WithdrawalsServiceClient(IXpertersAdminServiceClient serviceClient)
        {
            _serviceClient = serviceClient ?? throw new System.ArgumentNullException(nameof(serviceClient));
        }
        public async Task<GetPaymentWithdrawalsResponse> GetAsync(GetPaymentWithdrawalsRequest request)
        {
            return await _serviceClient.GetAsync(request);
        }
    }
}
