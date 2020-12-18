using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Refit;
using xperters.configurations;
using xperters.payments.Handlers;
using xperters.payments.Services.Interfaces;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;

namespace xperters.payments.Services
{
    public class MomoCollectionService : IMomoCollectionService
    {
        private readonly IMomoCollectionHandler _restService;
        private string _authorization;
        private readonly string _collectionUserId;
        private readonly string _collectionUserSecretKey;
        private readonly string _collectionSubscriptionKey;
        private readonly ILogger<MomoCollectionService> _logger;
        private readonly IAsyncPolicy _policy;
        private readonly string _momoEnvironment;
        private const int RetryCount = 5;

        public MomoCollectionService(AppConfig config, ILoggerFactory loggerFactory)
        {
            var paymentSettings = config.MomoPaymentSettings;
            _momoEnvironment = config.MomoPaymentSettings.Environment;
            _restService = RestService.For<IMomoCollectionHandler>(paymentSettings.BaseUri);
            _collectionUserId = paymentSettings.Collection.UserId;
            _collectionUserSecretKey = paymentSettings.Collection.UserSecretKey;
            _collectionSubscriptionKey = paymentSettings.Collection.SubscriptionKey;
            CreateBase64Credentials();

            _logger = loggerFactory.CreateLogger<MomoCollectionService>();

            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: RetryCount);

            _policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(delay, (e, i) => { _logger.LogError($"Error '{e.Message}' at retry #{i}"); });

        }

        private void CreateBase64Credentials()
        {
            var creds = string.Join(":", _collectionUserId, _collectionUserSecretKey);
            var buffer = Encoding.ASCII.GetBytes(creds);
            var credsEncodedBase64 = Convert.ToBase64String(buffer);
            _authorization = $"Basic {credsEncodedBase64}";
        }

        public async Task<AccessToken> GetAccessTokenAsync()
        {

            return await _policy.ExecuteAsync(async () =>
            {
                var response = await _restService.GetAccessToken(_collectionSubscriptionKey, _authorization);

                var token = new AccessToken
                {
                    Expires = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresInSeconds).AddSeconds(-5),
                    Token = response.AccessToken
                };

                _logger.LogDebug("completed a token request");

                return token;
            });

        }

        public async Task MakeRequestToPayAsync(RequestPayer requestPayer)
        {
            var tokenResult = await GetAccessTokenAsync();
            var authorization = $"Bearer {tokenResult.Token}";

            await _restService.MakeRequestToPay(_collectionSubscriptionKey, authorization, _momoEnvironment, requestPayer.ExternalId, requestPayer);

            _logger.LogDebug($"made request to pay for {requestPayer.ExternalId} for {requestPayer.Payer.PartyId}. Amount: {requestPayer.Amount} Currency: {requestPayer.Currency} Reference: {requestPayer.ExternalId}");
        }

        public async Task<RequestToPayResponse> GetRequestToPayStatusAsync(Guid requestReference)
        {

            return await _policy.ExecuteAsync(async () =>
            {
                var tokenResult = await GetAccessTokenAsync();
                var authorization = $"Bearer {tokenResult.Token}";

                var response = await _restService.GetRequestToPayStatus(_collectionSubscriptionKey, authorization, _momoEnvironment, requestReference);

                _logger.LogDebug($"Request to pay status for {requestReference} for {response.Payee.PartyId}. Amount: {response.Amount} Currency: {response.Currency} Reference: {response.ExternalId} FinancialTransactionId: {response.FinancialTransactionId}");

                return response;
            });
        }


        public async Task<AccountBalance> GetAccountBalanceAsync()
        {
            return await _policy.ExecuteAsync (GetAccountBalanceRetryAsync);
        }

        private async Task<AccountBalance> GetAccountBalanceRetryAsync()
        {
            var tokenResult = await GetAccessTokenAsync();
            var authorization = $"Bearer {tokenResult.Token}";

            var response = await _restService.GetAccountBalance(_collectionSubscriptionKey, authorization, _momoEnvironment);

            var token = new AccountBalance
            {
                Balance = response.Balance,
                Currency = response.Currency
            };

            _logger.LogInformation($"Account balance status Currency: {response.Currency} Balance: {response.Balance}");

            return token;
        }
    }
}
