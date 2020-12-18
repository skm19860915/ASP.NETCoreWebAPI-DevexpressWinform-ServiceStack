
using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class RequestPayee
    {
        [JsonProperty(PropertyName = "payee")]
        public Payee Payee { get; set; }

        [JsonProperty(PropertyName = "payeeNote")]
        public string PayerNote { get; set; }

        [JsonProperty(PropertyName = "payerMessage")]
        public string PayerMessage { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }
    }
}
