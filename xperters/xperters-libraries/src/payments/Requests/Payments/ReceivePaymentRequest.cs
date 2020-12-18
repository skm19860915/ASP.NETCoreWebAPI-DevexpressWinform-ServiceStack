using Newtonsoft.Json;

namespace xperters.payments.Requests.Payments
{
    public class ReceivePaymentRequest
    {
        public ReceivePaymentRequest()
        {
            Customer = new Customer();
            Payment = new Payment();
        }

        public Customer Customer { get; set; }
        [JsonProperty("transaction")]
        public Payment Payment { get; set; }
    }
}
