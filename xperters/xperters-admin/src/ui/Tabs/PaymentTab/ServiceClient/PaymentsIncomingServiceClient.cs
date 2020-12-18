using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Incoming;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.PaymentTab.ServiceClient
{
    public class PaymentsIncomingServiceClient : IPaymentsIncomingServiceClient
    {
        private readonly IXpertersAdminServiceClient _serviceClient;
        public PaymentsIncomingServiceClient(IXpertersAdminServiceClient serviceClient)
        {
            _serviceClient = serviceClient ?? throw new System.ArgumentNullException(nameof(serviceClient));
        }
        public async Task<GetPaymentIncomingResponse> GetAsync(GetPaymentIncomingRequest request)
        {
            return await _serviceClient.GetAsync(request);
        }
    }
}
