using System;
using System.Threading.Tasks;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Services.Interfaces
{
    public interface IMomoCollectionService
    {
        Task<AccessToken> GetAccessTokenAsync();
        Task<AccountBalance> GetAccountBalanceAsync();
        Task MakeRequestToPayAsync(RequestPayer requestPayer);
        Task<RequestToPayResponse> GetRequestToPayStatusAsync(Guid requestReference);
    }
}