using Newtonsoft.Json;

namespace xperters.payments.Requests.Payments
{
    public class Customer
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode{ get; set; }
    }
}
