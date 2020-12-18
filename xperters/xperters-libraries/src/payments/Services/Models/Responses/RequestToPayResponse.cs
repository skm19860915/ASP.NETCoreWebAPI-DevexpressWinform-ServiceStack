using Newtonsoft.Json;
using xperters.payments.Services.Models.Internal;

namespace xperters.payments.Services.Models.Responses
{
    public class TransferResponse
    {
        [JsonProperty(PropertyName = "payee")]
        public Payee Payee { get; set; }

        [JsonProperty(PropertyName = "payeeNote")]
        public string PayerNote { get; set; }

        [JsonProperty(PropertyName = "payerMessage")]
        public string PayerMessage { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "financialTransactionId")]
        public string FinancialTransactionId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }
        [JsonProperty(PropertyName = "reason")]
        public Reason Reason { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
