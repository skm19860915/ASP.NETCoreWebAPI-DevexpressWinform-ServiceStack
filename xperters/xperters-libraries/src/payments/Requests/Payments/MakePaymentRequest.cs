using Newtonsoft.Json;

namespace xperters.payments.Requests.Payments
{
    public class MakePaymentRequest
    {
        public MakePaymentRequest()
        {
            Source = new PaymentSource();
            Destination = new PaymentDestination();
            Payment = new PaymentSend();
        }

        public PaymentSource Source { get; set; }
        public PaymentDestination Destination { get; set; }
        [JsonProperty("transfer")]
        public PaymentSend Payment { get; set; }
    }
}

//    {
//        "source": {
//            "countryCode": "KE",
//            "name": "John Doe",
//            "accountNumber": "0011547896523"
//        },
//        "destination": {
//            "type": "mobile",
//            "countryCode": "KE",
//            "name": "Tom Doe",
//            "bankCode": "01",
//            "mobileNumber": "0722000000"
//        },
//        "transfer": {
//            "type": "PesaLink",
//            "amount": "40.00",
//            "currencyCode": "KES",
//            "reference": "692194625798",
//            "date": "2018-04-30",
//            "description": "Some remarks here"
//        }
//    }