using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Responses
{
    public class AccountBalanceResponse
    {
        [JsonProperty(PropertyName = "availableBalance")]
        public decimal Balance { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
    }
}