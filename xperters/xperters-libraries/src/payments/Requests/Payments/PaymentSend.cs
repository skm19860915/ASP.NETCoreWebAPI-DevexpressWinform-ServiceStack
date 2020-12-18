
using Newtonsoft.Json;

namespace xperters.payments.Requests.Payments
{
    public class PaymentSend : Payment
    {
        public string Date { get; set; }
        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

    }
}