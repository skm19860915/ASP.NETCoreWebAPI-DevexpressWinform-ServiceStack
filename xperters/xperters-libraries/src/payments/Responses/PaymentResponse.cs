using Newtonsoft.Json;

namespace xperters.payments.Responses
{
    public class PaymentResponse
    {
        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
