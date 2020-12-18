using Newtonsoft.Json;

namespace xperters.payments.Requests.Payments
{
    public class PaymentSource
    {
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        public string Name { get; set; }
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }
    }
}