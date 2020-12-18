using System;
using System.Threading.Tasks;
using Refit;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Handlers
{
    public interface IMomoCollectionHandler
    {
        [Post("/collection/token/")]
        Task<AccessTokenResponse> GetAccessToken([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, [Header("Authorization")] string authorization);

        [Get("/collection/v1_0/account/balance")]
        [Headers("Content-Type: application/json")]
        Task<AccountBalanceResponse> GetAccountBalance([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, 
                                                        [Header("Authorization")] string authorization, 
                                                        [Header("X-Target-Environment")] string environment);

        [Post("/collection/v1_0/requesttopay")]
        [Headers("Content-Type: application/json")]
        Task MakeRequestToPay([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, 
                            [Header("Authorization")] string authorization, 
                            [Header("X-Target-Environment")] string environment, 
                            [Header("X-Reference-Id")] string referenceId,
                            [Body]RequestPayer requestPayer);

        [Get("/collection/v1_0/requesttopay/{referenceId}")]
        [Headers("Content-Type: application/json")]
        Task<RequestToPayResponse> GetRequestToPayStatus([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey,
            [Header("Authorization")] string authorization,
            [Header("X-Target-Environment")] string environment,
            Guid referenceId);
    }
}
