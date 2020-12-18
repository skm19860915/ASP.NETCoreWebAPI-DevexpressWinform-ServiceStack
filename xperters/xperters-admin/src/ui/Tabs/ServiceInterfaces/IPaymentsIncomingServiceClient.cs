using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Incoming;

namespace Xperters.Admin.UI.Tabs.ServiceInterfaces
{
    public interface IPaymentsIncomingServiceClient
    {
        Task<GetPaymentIncomingResponse> GetAsync(GetPaymentIncomingRequest request);
    }
}
