using Newtonsoft.Json;

namespace xperters.payments.Services.Models.Internal
{
    public class TransactionReferencePayment
    {
        [JsonProperty(PropertyName = "transaction_ref")]
        public string Reference;
    }
}