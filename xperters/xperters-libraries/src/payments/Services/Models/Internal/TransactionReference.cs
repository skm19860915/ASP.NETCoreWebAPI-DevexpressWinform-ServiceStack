using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class TransactionReference
    {
        [JsonProperty(PropertyName = "transaction_ref")]
        public string Reference;
    }
}