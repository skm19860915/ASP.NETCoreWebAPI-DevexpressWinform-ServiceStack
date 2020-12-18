namespace xperters.payments.Requests.Payments
{
    public class PaymentDestination
    {
        public string Type { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
        public string BankCode { get; set; }
        public string MobileNumber { get; set; }

    }
}