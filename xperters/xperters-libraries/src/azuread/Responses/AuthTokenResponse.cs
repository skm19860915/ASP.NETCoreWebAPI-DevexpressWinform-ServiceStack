using System;
using Newtonsoft.Json;

namespace xperters.azuread.Responses
{
    public class AuthTokenResponse
    {
        public AuthTokenResponse()
        {
            ExpiresOnInConverted = DateTime.MinValue;
        }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public string ExtExpiresIn { get; set; }

        [JsonProperty("expires_on")]
        public string ExpiresOn { get; set; }
        public DateTime ExpiresOnInConverted { get; set; }

        [JsonProperty("not_before")]
        public string NotBefore { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
