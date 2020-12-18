using System.Threading.Tasks;
using xperters.payments.Requests;
using xperters.payments.Requests.Payments;
using xperters.payments.Responses;

namespace xperters.payments.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentTokenResponse> GetAuthorizationToken();
        Task<AccountBalancesResponse> GetAccountBalance(string token, AccountBalanceRequest request);

        Task<PaymentResponse> MakePaymentToMobileNumber(string token, MakePaymentRequest request);
        Task<PaymentResponse> ReceivePaymentFromEazzypay(string token, ReceivePaymentRequest request);
    }
}
