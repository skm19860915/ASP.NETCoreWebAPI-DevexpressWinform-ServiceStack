using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using xperters.configurations;
using xperters.configurations.Settings.Payments;
using xperters.http.Interfaces;
using xperters.payments.Extensions;
using xperters.payments.Interfaces;
using xperters.payments.Requests;
using xperters.payments.Requests.Payments;
using xperters.payments.Responses;

namespace xperters.payments.Services
{
    /// <summary>
    /// Wallet service needs to sign more requests with a private key
    /// so that they can be authenticated by the provider
    /// </summary>
    public class JengaPaymentService : IPaymentService
    {
        private readonly ISigningService _signingService;
        private readonly IHttpHandler _httpHandler;
        private readonly JengaSettings _jengaPaymentSettings;

        public JengaPaymentService(IHttpHandler httpHandler, ISigningService signingService, AppConfig appConfig)
        {
            _signingService = signingService;
            _jengaPaymentSettings = appConfig.PaymentSettings.Jenga;
            _httpHandler = httpHandler;
        }

        public async Task<PaymentTokenResponse> GetAuthorizationToken()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", _jengaPaymentSettings.AccountNumber),
                new KeyValuePair<string, string>("password", _jengaPaymentSettings.Password)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            // https://uat.jengahq.io/identity/v2/token
            var url = $"{_jengaPaymentSettings.ApiBaseUrl}identity/v2/token";

            var response = await _httpHandler.PostAsyncWithAuthorization(_jengaPaymentSettings.ApiKey, url, content);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString.FromJson<PaymentTokenResponse>();
        }

        /// <summary>
        /// Get the account balance
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AccountBalancesResponse> GetAccountBalance(string token, AccountBalanceRequest request)
        {

            // Serialize our concrete class into a JSON String          
            var json = request.ToJson();

            var message = $"{request.CountryCode}{_jengaPaymentSettings.SubscriptionApiId}";

            // sign the message so the signed value can be sent with the request to the provider
            var signedData = _signingService.SignData(message);

            var headersList = new List<(string name, string value)>
            {
                (name: "authorization", value:$"Bearer {token}"),
                (name: "signature", value: signedData)
            };

            // https://uat.jengahq.io/account/v2/accounts/balances/KE/0011547896523
            var url = $"{_jengaPaymentSettings.ApiBaseUrl}account/v2/accounts/balances/KE/{_jengaPaymentSettings.SubscriptionApiId}";
          
            var response = await _httpHandler.GetAsyncWithHeaders(headersList, url);

            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString.FromJson<AccountBalancesResponse>();
        }

        /// <summary>
        /// Send funds to a mobile number
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaymentResponse> MakePaymentToMobileNumber(string token, MakePaymentRequest request)
        {
            // Serialize our concrete class into a JSON String          
            var json = request.ToJson();

            var message = request.Payment.Amount +
                          request.Payment.CurrencyCode +
                          request.Payment.Reference +
                          request.Source.AccountNumber;

            // sign the message so the signed value can be sent with the request to the provider
            var signedData = _signingService.SignData(message);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var headersList = new List<(string name, string value)>
            {
                (name: "authorization", value:$"Bearer {token}"),
                (name: "signature", value: signedData)
            };

            // https://uat.jengahq.io/transaction/v2/remittance

            var url = $"{_jengaPaymentSettings.ApiBaseUrl}transaction/v2/remittance";
          
            var response = await _httpHandler.PostAsyncWithHeaders(headersList, url, content);

            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString.FromJson<PaymentResponse>();
        }

        /// <summary>
        /// Send funds to a mobile number
        /// </summary>
        /// <param name="token"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PaymentResponse> ReceivePaymentFromEazzypay(string token, ReceivePaymentRequest request)
        {
            // Serialize our concrete class into a JSON String          
            var json = request.ToJson();

            var message = request.Payment.Reference +
                          request.Payment.Amount +
                          _jengaPaymentSettings.AccountNumber +
                          request.Customer.CountryCode;

            // sign the message so the signed value can be sent with the request to the provider
            var signedData = _signingService.SignData(message);

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var headersList = new List<(string name, string value)>
            {
                (name: "authorization", value:$"Bearer {token}"),
                (name: "signature", value: signedData)
            };

            // https://uat.jengahq.io/transaction/v2/payments

            var url = $"{_jengaPaymentSettings.ApiBaseUrl}transaction/v2/payments";
          
            var response = await _httpHandler.PostAsyncWithHeaders(headersList, url, content);

            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString.FromJson<PaymentResponse>();
        }
    }
}
