using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using xperters.azuread.Interfaces;
using xperters.azuread.Responses;

namespace xperters.azuread.Services
{
    public class MobileNumberService : IMobileNumberService
    {
        private readonly string AuthorityUrl = @"https://login.microsoftonline.com/b8bccc9d-2910-469a-96bf-78ae7e1e7b33/oauth2/token/";
        private readonly Uri _oauthEndpoint;
        const string ResourceUri = "https://management.core.windows.net/";
        private const string Email = "mobileserviceuser@xpertersdevlocal.onmicrosoft.com";
        private const string Password = "o3QHtNpPwR#9648m";
        private const string ClientId = "1950a258-227b-4e31-a9cf-717495945fc2";   // Hardcoded PowerShell value do not change.

        public MobileNumberService()
        {
            _oauthEndpoint = new Uri(AuthorityUrl);

        }

        public async Task<string> GetAzureRefreshToken()
        {
            using var client = new HttpClient();

            var result = await client.PostAsync(_oauthEndpoint, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("resource", ResourceUri),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", Email),
                    new KeyValuePair<string, string>("password", Password),
                    new KeyValuePair<string, string>("scope", "openid"),
                }
            ));

            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<AuthTokenResponse>(content);

            return response.RefreshToken;
        }

        public async Task<string> GetIamAdAccessToken()
        {
            var refreshToken = await GetAzureRefreshToken();

            var resourceId = "74658136-14ec-4630-ad9b-26e160ff0fc6";
            using var client = new HttpClient();

            var result = await client.PostAsync(_oauthEndpoint, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("resource", resourceId),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("scope", "openid"),
                }
            ));

            var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<AuthTokenResponse>(content);

            return response.AccessToken;
        }

        public async Task<UserDetailResponse> GetUserDetail()
        {
            var accessToken = await GetIamAdAccessToken();

            var url = "https://main.iam.ad.ext.azure.com/api/UserDetails/995a1e22-6348-42ed-8f9f-92220562445f";
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            client.DefaultRequestHeaders.Add("x-ms-client-request-id", Guid.NewGuid().ToString());
            client.DefaultRequestHeaders.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.GetStringAsync(url);

            var resp = JsonConvert.DeserializeObject<UserDetailResponse>(result);

            return resp;
        }

        public async Task<string> GetUserMobileNumber()
        {
            var response = await GetUserDetail();

            return response.AuthenticationPhoneNumber;
        }
    }
}
