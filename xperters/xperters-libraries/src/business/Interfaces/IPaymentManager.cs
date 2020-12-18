using System;
using System.Threading.Tasks;
using xperters.domain;
using xperters.entities.Entities;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.business.Interfaces
{
    public interface IPaymentManager
    {
        Task<AccessToken> GetAccessToken();
        Task<AccountBalance> GetAccountBalance();
        Task MakeRequestToPayAsync(RequestPayer requestPayer);
        Task<RequestToPayResponse> GetRequestToPayStatus(Guid requestReference);
        MilestoneRequestPayer SubmitMilestoneRequestPayer(MilestoneRequestPayerDto milestoneRequestPayerDto);
        string GetPhoneNumber();

        Guid GetUserId();

        bool UpdateMilestoneRequest(Guid milestoneId, Guid id);
    }
}
