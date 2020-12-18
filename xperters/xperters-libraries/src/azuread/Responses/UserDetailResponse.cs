using Newtonsoft.Json;

namespace xperters.azuread.Responses
{
    public class UserDetailResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }
        
        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty("authenticationPhoneNumber")]
        public string AuthenticationPhoneNumber { get; set; }

        [JsonProperty("displayUserPrincipalName")]
        public string DisplayUserPrincipalName { get; set; }

        [JsonProperty("objectId")]
        public string ObjectId { get; set; }
        [JsonProperty("city")]

        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("businessPhones")]
        public string[] BusinessPhones { get; set; }

    }
}
