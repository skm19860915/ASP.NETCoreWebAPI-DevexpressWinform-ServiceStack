using Newtonsoft.Json;

namespace xperters.payments.Requests
{
    public class AccountBalanceRequest
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }
}


