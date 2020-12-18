using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using xperters.azuread.Interfaces;
using xperters.azuread.Requests;
using xperters.azuread.Responses;
using xperters.configurations;
using xperters.configurations.Settings.Ad;
using xperters.constants;
using xperters.enums;
using xperters.extensions;
using xperters.http.Interfaces;

namespace xperters.azuread.Handlers
{
    public class AdAuthHandler : IHandleAdAuth
    {
        private readonly IHttpHandler _httpHandler;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AzureAdAppRegSettings _settings;
        private readonly AzureAdB2CSettings _b2CSettings;
        private readonly ILogger<AdAuthHandler> _logger;

        public AdAuthHandler(IHostEnvironment env, IHttpHandler httpHandler, AppConfig config, ILoggerFactory loggerFactory)
        {
            _httpHandler = httpHandler;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<AdAuthHandler>();
            _settings = config.AzureAdAppRegSettings;
            _b2CSettings = config.AzureAdB2CSettings;
        }

        public void AddTokenToRequestHeaders(string token)
        {
            _httpHandler.RequestHeaders.Clear();
            _httpHandler.RequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<UserDetailResponse> GetAadUserDetails(string userId, string graphEndpoint)
        {
            _logger.LogDebug($"Attempting to retrieve details for user {userId}");

            var accessToken = await GetAccessToken();

            if (accessToken.IsBlank())
            {
                throw new Exception("No token retrieved for authentication request");
            }

            var url = $"{graphEndpoint}/{userId}";

            var headersList = new List<(string name, string value)>
            {
                (name: "ContentType", value:"application/json"),
                (name: "Authorization", value:$"Bearer {accessToken}")
            };

            var response = await _httpHandler.GetAsyncWithHeaders(headersList, url);
            _logger.LogDebug($"Called {url} api.  Response is: {response.StatusCode}");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            var userDetailsResponse = JsonConvert.DeserializeObject<UserDetailResponse>(result);
            _logger.LogDebug($"User {userDetailsResponse.DisplayName} display name has been retrieved;");

            return userDetailsResponse;
        }

        public async Task UpdateAadUserDetails(string userId, UserDetailsRequest userDetails, string graphEndpoint)
        {
            _logger.LogDebug($"User {userId} display name is blank and needs to be set;");

            var accessToken = await GetAccessToken();

            AddTokenToRequestHeaders(accessToken);

            var json = userDetails.ToJson();

            var url = $"{graphEndpoint}/{userId}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpHandler.PatchAsync(url, content);
            result.EnsureSuccessStatusCode();

            _logger.LogDebug($"User details for {userDetails.DisplayName} have been set;");
        }

        public async Task<AuthTokenResponse> GetTokenForGraphAccess(string grantType)
        {

            _logger.LogDebug($"Attempting authentication to {_settings.MsGraphScope}");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("scope", _settings.MsGraphScope),
                new KeyValuePair<string, string>("client_id", _settings.ClientId),
                new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
                new KeyValuePair<string, string>("grant_type", grantType)
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var url = $"{_settings.MsOnlineTokenUrl}/{_settings.TenantId}/{AzureConstants.TokenEndpointSuffix}";

            var response = await _httpHandler.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var tokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(result);
            return tokenResponse;
        }

        
        public async Task<string> GetUserMobileNumberFromAuthContact(string userId)
        {
            var response = await GetTokenForGraphAccess(AadGrantTypes.ClientCredentials);

            _logger.LogDebug("Attempting to retrieve user's mobile number");
            var userDetails = await GetUserMobileNumber(userId, response.AccessToken);

            return userDetails.MobilePhone;
        }

        private async Task<UserDetailResponse> GetUserMobileNumber(string userId, string token)
        {
            _logger.LogDebug($"User {userId} display name is blank and needs to be set;");

            var headersList = new List<(string name, string value)>
            {
                (name: "ContentType", value:"application/json"),
                (name: "Authorization", value:$"Bearer {token}"),
            };

            var url = $"{_settings.MsGraphUrl}/{userId}";
            var message = await _httpHandler.GetAsyncWithHeaders(headersList, url);
            var result = await message.Content.ReadAsStringAsync();
            var userDetailsResponse = JsonConvert.DeserializeObject<UserDetailResponse>(result);

            return userDetailsResponse;
        }

        public async Task<string> GetAccessToken()
        {
            // Get OAuth token using client credentials 
            string authString = $"{_settings.MsOnlineTokenUrl}/{_settings.TenantId}";

            var context = new AuthenticationContext(authString, false);

            // Config for OAuth client credentials  
            var clientCred = new ClientCredential(_settings.ClientId, _settings.ClientSecret);
            var token = string.Empty;

            try
            {
                var authenticationResult = await context.AcquireTokenAsync(_b2CSettings.GraphUri, clientCred);
                token = authenticationResult.AccessToken;
            }
            catch (AuthenticationException ex)
            {
                Debug.WriteLine("Acquiring a token failed with the following error: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    //  You should implement retry and back-off logic according to
                    //  http://msdn.microsoft.com/en-us/library/dn168916.aspx . This topic also
                    //  explains the HTTP error status code in the InnerException message. 
                    Debug.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
            }
            return token;
        }

        public async Task<Enums.UserRole> UpdateUserRole(string userId, Enums.UserRole role)
        {
            var client = new B2CGraphClient(_settings, _b2CSettings.Tenant, _loggerFactory);

            string result = await client.GetExtensionApplicationAsync();
            var json = JObject.Parse(result);

            var objectId = json["value"][0]["objectId"].ToString();

            string extensionResult = await client.GetExtensionsAsync(objectId);
            _logger.LogDebug($"Found user data with objectId: {objectId}");
            var json1 = JObject.Parse(extensionResult);

            if (json1["value"][0].ToString().IsBlank())
            {
                throw new ArgumentNullException("UserRole attribute has not been added to B2C tenant for {_b2CSettings.Tenant}");
            }

            var customFieldName = json1["value"][0]["name"];
            var userRoleName = customFieldName?.ToString();

            var user = await client.GetUserByObjectIdAsync(userId);
            var roleResult = user[userRoleName];

            if (roleResult == null || !Enum.TryParse<Enums.UserRole>(roleResult.ToString(), out var parsedRole) || parsedRole != role)
            {
                var jsonUpdate = new JObject
                {
                    {userRoleName, role.ToString()}
                };

                await client.UpdateUserAsync(userId, jsonUpdate.ToString());

                _logger.LogDebug($"role {role} has been updated for user {userId}");
                user = await client.GetUserByObjectIdAsync(userId);

                Enum.TryParse(user[userRoleName].ToString(), out parsedRole);
            }

            _logger.LogDebug($"role {role} now exists for user {userId} (existing value is: {parsedRole})");

            return parsedRole;
        }
    }
}
