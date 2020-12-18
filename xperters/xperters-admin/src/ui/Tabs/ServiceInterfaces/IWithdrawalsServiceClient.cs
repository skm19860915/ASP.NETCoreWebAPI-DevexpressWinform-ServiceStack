using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Withdrawals;

namespace Xperters.Admin.UI.Tabs.ServiceInterfaces
{
    public interface IWithdrawalsServiceClient
    {
        Task<GetPaymentWithdrawalsResponse> GetAsync(GetPaymentWithdrawalsRequest request);
    }
}
