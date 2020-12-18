using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xperters.configurations.Settings.Ad;
using xperters.extensions;

namespace xperters.azuread.Handlers
{
    public class B2CGraphClient
    {
        private readonly string _tenant;
        private readonly AzureAdAppRegSettings _settings;

        private readonly AuthenticationContext _authContext;
        private readonly ClientCredential _credential;
        private readonly ILogger<B2CGraphClient> _logger;

        public B2CGraphClient(AzureAdAppRegSettings settings, string tenant,  ILoggerFactory loggerFactory)
        {
            _tenant = tenant;
            _settings = settings;
            _logger = loggerFactory.CreateLogger<B2CGraphClient>();

            // The AuthenticationContext is ADAL's primary class, in which you indicate the directory to use.
            var authority = $"{_settings.MsOnlineTokenUrl}/{tenant}";
            _authContext = new AuthenticationContext(authority);
            _logger.LogDebug($"Authority used for Graph is:  {authority}");

            // The ClientCredential is where you pass in your client_id and client_secret, which are 
            // provided to Azure AD in order to receive an access_token using the app's identity.
            _credential = new ClientCredential(settings.ClientId, settings.ClientSecret);
        }

        public async Task<JObject> GetUserByObjectIdAsync(string objectId)
        {
            var user =  await SendGraphGetRequest("/users/" + objectId, null);

            return JObject.Parse(user);
        }

        public async Task<string> UpdateUserAsync(string objectId, string json)
        {
            return await SendGraphPatchRequest("/users/" + objectId, json);
        }

        public async Task<string> GetExtensionsAsync(string appObjectId)
        {
            var extensions = await SendGraphGetRequest("/applications/" + appObjectId + "/extensionProperties", null);

            return extensions;
        }

        public async Task<string> GetExtensionApplicationAsync()
        {
            const string query = "$filter=startswith(displayName, 'b2c-extensions-app')";
            var applicationsAsync = await SendGraphGetRequest("/applications", query);

            return applicationsAsync;
        }

        private async Task<string> SendGraphPatchRequest(string api, string json)
        {
            var result = await _authContext.AcquireTokenAsync(_settings.MsGraphNetUrl, _credential);
            var http = new HttpClient();
            var url = _settings.MsGraphNetUrl + _tenant + api + "?" + _settings.MsGraphApiVersion;

            _logger.LogDebug("PATCH " + url);
            _logger.LogDebug("Content-Type: application/json");
            _logger.LogDebug(json);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);
                _logger.LogError("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));

                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            _logger.LogDebug((int)response.StatusCode + ": " + response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> SendGraphPostRequest(string api, string json)
        {
            var result = await _authContext.AcquireTokenAsync(_settings.MsGraphNetUrl, _credential);
            var http = new HttpClient();
            var url = _settings.MsGraphNetUrl + _tenant + api + "?" + _settings.MsGraphApiVersion;

            _logger.LogDebug("POST " + url);
            _logger.LogDebug("Content-Type: application/json");
            _logger.LogDebug(json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);
                var message = "Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented);
                _logger.LogError(message);
                throw new WebException(message);
            }

            _logger.LogDebug((int)response.StatusCode + ": " + response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendGraphGetRequest(string api, string query)
        {
            // First, use ADAL to acquire a token using the app's identity (the credential)
            // The first parameter is the resource we want an access_token for; in this case, the Graph API.
            var result = await _authContext.AcquireTokenAsync(_settings.MsGraphNetUrl, _credential);

            // For B2C user management, be sure to use the 1.6 Graph API version.
            var http = new HttpClient();
            var url = _settings.MsGraphNetUrl + _tenant + api + "?" + _settings.MsGraphApiVersion;
            if (!string.IsNullOrEmpty(query))
            {
                url += "&" + query;
            }

            _logger.LogDebug("GET " + url);

            // Append the access token for the Graph API to the Authorization header of the request, using the Bearer scheme.
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var formatted = JsonConvert.DeserializeObject(error);
                var message = "Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented);
                _logger.LogError(message);
                throw new WebException(message);
            }

            _logger.LogDebug((int)response.StatusCode + ": " + response.ReasonPhrase);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
