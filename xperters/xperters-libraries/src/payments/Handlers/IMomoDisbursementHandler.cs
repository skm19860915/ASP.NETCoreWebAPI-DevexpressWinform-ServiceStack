﻿using System;
using System.Threading.Tasks;
using Refit;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Handlers
{
    public interface IMomoDisbursementHandler
    {
        [Post("/disbursement/token/")]
        Task<AccessTokenResponse> GetAccessToken([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, [Header("Authorization")] string authorization);

        [Get("/disbursement/v1_0/account/balance")]
        [Headers("Content-Type: application/json")]
        Task<AccountBalanceResponse> GetAccountBalance([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, 
                                                        [Header("Authorization")] string authorization, 
                                                        [Header("X-Target-Environment")] string environment);

        [Post("/disbursement/v1_0/transfer")]
        [Headers("Content-Type: application/json")]
        Task MakeTransfer([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey, 
                            [Header("Authorization")] string authorization, 
                            [Header("X-Target-Environment")] string environment, 
                            [Header("X-Reference-Id")] string referenceId,
                            [Body]RequestPayee requestPayee);

        [Get("/disbursement/v1_0/transfer/{referenceId}")]
        [Headers("Content-Type: application/json")]
        Task<TransferResponse> GetTransferStatus([Header("Ocp-Apim-Subscription-Key")] string subscriptionKey,
            [Header("Authorization")] string authorization,
            [Header("X-Target-Environment")] string environment,
            Guid referenceId);

    }
}