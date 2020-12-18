using Newtonsoft.Json;

namespace xperters.azuread.Requests
{
    public class UserDetailsRequest
    {
        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
