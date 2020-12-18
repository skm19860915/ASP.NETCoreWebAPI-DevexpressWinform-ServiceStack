using System;
using System.Threading.Tasks;
using xperters.payments.Services.Models.Internal;

namespace xperters.payments.Services.Interfaces
{
    public interface IMomoDisbursementService
    {
        Task<AccessToken> GetAccessToken();
        Task<AccountBalance> GetAccountBalance();
        Task<Guid> MakeTransfer(RequestPayee requestPayee);
    }
}