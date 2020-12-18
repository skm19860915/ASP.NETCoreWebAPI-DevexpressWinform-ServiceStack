using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Refit;
using xperters.configurations;
using xperters.configurations.Settings.Payments;
using xperters.payments.Handlers;
using xperters.payments.Services.Interfaces;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Services
{
    public class MomoDisbursementService : IMomoDisbursementService
    {
        private readonly IMomoDisbursementHandler _restService;
        private string _authorization;
        private readonly string _disbursementUserSecretKey;
        private readonly string _disbursementUserId;
        private readonly string _disbursementSubscriptionKey;
        private readonly ILogger<MomoDisbursementService> _logger;
        private string _momoEnvironment;
        private const int RetryCount = 5;

        public MomoDisbursementService(AppConfig config, ILoggerFactory loggerFactory)
        {
            var paymentSettings = config.MomoPaymentSettings;
            _momoEnvironment = config.MomoPaymentSettings.Environment;
            _restService = RestService.For<IMomoDisbursementHandler>(paymentSettings.BaseUri);

            _disbursementUserSecretKey = paymentSettings.Disbursement.UserSecretKey;
            _disbursementUserId = paymentSettings.Disbursement.UserId;
            _disbursementSubscriptionKey = paymentSettings.Disbursement.SubscriptionKey;
            CreateBase64Credentials();

            _logger = loggerFactory.CreateLogger<MomoDisbursementService>();

        }

        private void CreateBase64Credentials()
        {
            var creds = string.Join(":", _disbursementUserId, _disbursementUserSecretKey);
            var buffer = Encoding.ASCII.GetBytes(creds);
            var credsEncodedBase64 = Convert.ToBase64String(buffer);
            _authorization = $"Basic {credsEncodedBase64}";

        }

        public async Task<AccessToken> GetAccessToken()
        {
            var response = await _restService.GetAccessToken(_disbursementSubscriptionKey, _authorization);

            var token = new AccessToken
            {
                Expires = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresInSeconds).AddSeconds(-5),
                Token = response.AccessToken
            };

            _logger.LogDebug("completed a token request");

            return token;
        }

        public async Task<Guid> MakeTransfer(RequestPayee requestPayee)
        {
            var tokenResult = await GetAccessToken();
            var authorization = $"Bearer {tokenResult.Token}";

            var requestReference = Guid.NewGuid();

            await _restService.MakeTransfer(_disbursementSubscriptionKey, authorization, _momoEnvironment, requestReference.ToString(), requestPayee);

            _logger.LogDebug($"made transfer request for {requestReference} for {requestPayee.Payee.PartyId}. Amount: {requestPayee.Amount} Currency: {requestPayee.Currency} Reference: {requestPayee.ExternalId}");

            return requestReference;
        }

        public async Task<TransferResponse> GetTransferStatus(Guid requestReference)
        {
            var tokenResult = await GetAccessToken();
            var authorization = $"Bearer {tokenResult.Token}";

            var response = await _restService.GetTransferStatus(_disbursementSubscriptionKey, authorization, _momoEnvironment, requestReference);

            _logger.LogDebug($"transfer status for {requestReference} for {response.Payee.PartyId}. Amount: {response.Amount} Currency: {response.Currency} Reference: {response.ExternalId} FinancialTransactionId: {response.FinancialTransactionId}");

            return response;
        }

        public async Task<AccountBalance> GetAccountBalance()
        {
            int currentRetry = 0;

            AccountBalance balance;
            var delay = TimeSpan.FromSeconds(5);

            for (; ; )
            {
                try
                {
                    // Call external service.
                    balance = await GetAccountBalanceRetry();



                    // Return or break.
                    break;
                }
                catch (ApiException ex)
                {
                    Trace.TraceError($"Operation Exception. Called: {RetryCount +1} times ");

                    var error = JsonConvert.DeserializeObject<Reason>(ex.Content);

                    _logger.LogError(error.Message);


                    currentRetry++;

                    // Check if the exception thrown was a transient exception
                    // based on the logic in the error detection strategy.
                    // Determine whether to retry the operation, as well as how
                    // long to wait, based on the retry strategy.
                    if (currentRetry > RetryCount || !IsTransient(ex))
                    {
                        // If this isn't a transient error or we shouldn't retry,
                        // rethrow the exception.
                        throw;
                    }
                }

                // Wait to retry the operation.
                // Consider calculating an exponential delay here and
                // using a strategy best suited for the operation and fault.
                await Task.Delay(delay);
            }

            return balance;
        }


        private bool IsTransient(Exception ex)
        {
            // Determine if the exception is transient.
            // In some cases this is as simple as checking the exception type, in other
            // cases it might be necessary to inspect other properties of the exception.
            if (ex is ApiException)
                return true;

            if (ex is WebException webException)
            {
                // If the web exception contains one of the following status values
                // it might be transient.
                return new[] {WebExceptionStatus.ConnectionClosed,
                        WebExceptionStatus.Timeout,
                        WebExceptionStatus.RequestCanceled }.
                    Contains(webException.Status);
            }

            // Additional exception checking logic goes here.
            return false;
        }

        private async Task<AccountBalance> GetAccountBalanceRetry()
        {
            var tokenResult = await GetAccessToken();
            var authorization = $"Bearer {tokenResult.Token}";

            var response = await _restService.GetAccountBalance(_disbursementSubscriptionKey, authorization, _momoEnvironment);

            var token = new AccountBalance
            {
                Balance = response.Balance,
                Currency = response.Currency
            };

            _logger.LogDebug($"Account balance status Currency: {response.Currency} Balance: {response.Balance}");

            return token;
        }
    }
}
