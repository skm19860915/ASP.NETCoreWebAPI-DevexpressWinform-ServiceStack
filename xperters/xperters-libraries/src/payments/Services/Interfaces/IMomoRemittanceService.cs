using System;
using System.Threading.Tasks;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Services.Interfaces
{
    public interface IMomoRemittanceService
    {
        Task<AccessToken> GetAccessToken();
        Task<AccountBalance> GetAccountBalance();
        Task<Guid> MakeTransfer(RequestPayee requestPayee);
        Task<TransferResponse> GetTransferStatus(Guid requestReference);
    }
}