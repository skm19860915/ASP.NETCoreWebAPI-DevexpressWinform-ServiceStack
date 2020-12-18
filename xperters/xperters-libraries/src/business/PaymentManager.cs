using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using xperters.business.Extensions;
using xperters.business.Interfaces;
using xperters.configurations;
using xperters.configurations.Settings.Payments;
using xperters.domain;
using xperters.entities.Entities;
using xperters.payments.Handlers;
using xperters.payments.Services.Models.Internal;
using xperters.payments.Services.Models.Responses;
using xperters.repositories;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;

namespace xperters.business
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IRepository<User> _accountsRepository;
        private readonly IMomoCollectionHandler _restService;
        private readonly TelemetryClient _telemetryClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MomoSettings _momoSettings;
        private string _authorization;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentManager> _logger;
        private const int RetryCount = 5;
        private readonly IRepository<MilestoneRequestPayer> _milestoneRequestPayerRepository;
        private readonly RetryPolicy _policy;
        private string _momoEnvironment;

        public PaymentManager(ILoggerFactory loggerFactory, TelemetryClient telemetryClient
                                , AppConfig appConfig, IRepository<User> accountsRepository
                                , IHttpContextAccessor httpContextAccessor, IMapper mapper
                                , IRepository<MilestoneRequestPayer> milestoneRequestPayerRepository)
        {
            _momoEnvironment = appConfig.MomoPaymentSettings.Environment;
            _telemetryClient = telemetryClient;
            _httpContextAccessor = httpContextAccessor;

            var httpClient = new HttpClient(new HttpLoggingHandler(/*new NativeMessageHandler()*/)) { BaseAddress = new Uri(appConfig.MomoPaymentSettings.BaseUri) };

            _restService = RestService.For<IMomoCollectionHandler>(httpClient);
            _momoSettings = appConfig.MomoPaymentSettings.Collection;
            _accountsRepository = accountsRepository;
            CreateBase64Credentials();
            _logger = loggerFactory.CreateLogger<PaymentManager>();
            _mapper = mapper;
            _milestoneRequestPayerRepository = milestoneRequestPayerRepository;
            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: RetryCount);

            _policy = Policy
                .Handle<ApiException>()
                .WaitAndRetryAsync(delay, (e, i) => { _logger.LogError($"Error '{e.Message}' at retry #{i}"); });

        }

        public MilestoneRequestPayer SubmitMilestoneRequestPayer(MilestoneRequestPayerDto milestoneRequestPayerDto)
        {
            var resultModel = new ResultModel<MilestoneRequestPayerDto>();
            var milestoneRequestPayer = _mapper.Map<MilestoneRequestPayer>(milestoneRequestPayerDto);
            _milestoneRequestPayerRepository.Add(milestoneRequestPayer);

            return milestoneRequestPayer;
        }

        private void CreateBase64Credentials()
        {
            var creds = string.Join(":", _momoSettings.UserId, _momoSettings.UserSecretKey);
            var buffer = Encoding.ASCII.GetBytes(creds);
            var credsEncodedBase64 = Convert.ToBase64String(buffer);
            _authorization = $"Basic {credsEncodedBase64}";
        }
        public async Task<AccessToken> GetAccessToken()
        {
            return await _policy.ExecuteAsync(async () =>
            {
                var response = await _restService.GetAccessToken(_momoSettings.SubscriptionKey, _authorization);
                var token = new AccessToken
                {
                    Expires = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresInSeconds).AddSeconds(-5),
                    Token = response.AccessToken
                };

                _logger.LogDebug("completed a token request");
                return token;
            });
        }
        public async Task<AccountBalance> GetAccountBalance()
        {
            return await _policy.ExecuteAsync(async () =>
            {
                var tokenResult = await GetAccessToken();
                var authorization = $"Bearer {tokenResult.Token}";
                var response = await _restService.GetAccountBalance(_momoSettings.SubscriptionKey, authorization, _momoEnvironment);
                var accountBalance = new AccountBalance
                {
                    Balance = response.Balance,
                    Currency = response.Currency
                };
                _telemetryClient.TrackEvent($"Account balance status Description: {response.Currency} Balance: {response.Balance}");
                return accountBalance;

            });
        }
        public async Task<RequestToPayResponse> GetRequestToPayStatus(Guid requestReference)
        {
            return await _policy.ExecuteAsync(async () =>
            {
                var tokenResult = await GetAccessToken();
                var authorization = $"Bearer {tokenResult.Token}";
                var response = await _restService.GetRequestToPayStatus(_momoSettings.SubscriptionKey, authorization,
                    _momoEnvironment, requestReference);

                _telemetryClient.TrackEvent($"Request to pay status for {requestReference} for {response.Payee.PartyId}. Amount: {response.Amount} Description: {response.Currency} Reference: {response.ExternalId} FinancialTransactionId: {response.FinancialTransactionId}");

                return response;
            });
        }

        public async Task MakeRequestToPayAsync(RequestPayer requestPayer)
        {
            await _policy.ExecuteAsync(async () =>
            {
                // Use the externalId (i.e. milestoneId as the reference).
                var tokenResult = await GetAccessToken();
                var authorization = $"Bearer {tokenResult.Token}";

                var json = JsonSerializer.Serialize(requestPayer,new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
                });

                Debug.WriteLine(json);

                await _restService.MakeRequestToPay(_momoSettings.SubscriptionKey, authorization, _momoEnvironment,
                    requestPayer.ExternalId, requestPayer);

                _telemetryClient.TrackEvent($"made request to pay for {requestPayer.ExternalId} for {requestPayer.Payer.PartyId}. Amount: {requestPayer.Amount} Description: {requestPayer.Currency} Reference: {requestPayer.ExternalId}");

            });
        }

        public string GetPhoneNumber()
        {
            try
            {
                var phoneNumber = string.Empty;
                var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
                Guid.TryParse(loggedInUserId, out var guid);

                var user = _accountsRepository.Get(m => m.Id == guid).FirstOrDefault();
                if (user != null)
                {
                    phoneNumber = user.MobilePhone.Replace("+", "");
                }
                return phoneNumber;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mobile number for user not found {ex.Message}");
                return null;
            }
        }
        public Guid GetUserId()
        {
            try
            {
                var loggedInUserId = _httpContextAccessor.HttpContext.User?.GetUserId();
                var guid = new Guid(loggedInUserId);
                return guid;
            }
            catch (Exception ex)
            {

                _logger.LogError($"UserID not found {ex.Message}");
                var id = Guid.NewGuid();
                return id;
            }
        }
        public bool UpdateMilestoneRequest(Guid milestoneId, Guid id)
        {
            try
            {
                var milestoneRequestPayer = _milestoneRequestPayerRepository.Get().First(x => x.Id == id);
                milestoneRequestPayer.MilestoneId = milestoneId;
                _milestoneRequestPayerRepository.Update(milestoneRequestPayer);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }

    public class HttpLoggingHandler : DelegatingHandler
    {
        public HttpLoggingHandler(HttpMessageHandler innerHandler = null) : base(
            innerHandler ?? new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            await Task.Delay(1, cancellationToken).ConfigureAwait(false);
            var start = DateTime.Now;
            var req = request;
            var msg = $"[{req.RequestUri.PathAndQuery} -  Request]";

            Debug.WriteLine($"{msg}========Request Start==========");
            Debug.WriteLine($"{msg} {req.Method} {req.RequestUri.PathAndQuery} {req.RequestUri.Scheme}/{req.Version}");
            Debug.WriteLine($"{msg} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host}");

            foreach (var header in req.Headers)
            {
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (req.Content != null)
            {
                foreach (var header in req.Content.Headers)
                {
                    Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
                }

                Debug.WriteLine($"{msg} Content:");

                if (req.Content is StringContent || IsTextBasedContentType(req.Headers) || IsTextBasedContentType(req.Content.Headers))
                {
                    var result = await req.Content.ReadAsStringAsync();

                    Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(256))}...");
                }
            }

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            Debug.WriteLine($"{msg}==========Request End==========");

            msg = $"[{req.RequestUri.PathAndQuery} - Response]";

            Debug.WriteLine($"{msg}=========Response Start=========");

            var resp = response;

            Debug.WriteLine($"{msg} {req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");

            foreach (var header in resp.Headers)
            {
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
            }

            if (resp.Content != null)
            {
                foreach (var header in resp.Content.Headers)
                {
                    Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");
                }

                Debug.WriteLine($"{msg} Content:");

                if (resp.Content is StringContent || IsTextBasedContentType(resp.Headers) || IsTextBasedContentType(resp.Content.Headers))
                {
                    var result = await resp.Content.ReadAsStringAsync();

                    Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(256))}...");
                }
            }

            Debug.WriteLine($"{msg} Duration: {DateTime.Now - start}");
            Debug.WriteLine($"{msg}==========Response End==========");
            return response;
        }

        readonly string[] types = { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        private bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string> values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}