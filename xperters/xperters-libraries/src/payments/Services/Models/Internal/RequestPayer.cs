
using System;
using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class RequestPayer
    {
        [JsonProperty(PropertyName = "payer")]
        public Payer Payer { get; set; }

        [JsonProperty(PropertyName = "payeeNote")]
        public string Note { get; set; }

        [JsonProperty(PropertyName = "payerMessage")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

    }
}
