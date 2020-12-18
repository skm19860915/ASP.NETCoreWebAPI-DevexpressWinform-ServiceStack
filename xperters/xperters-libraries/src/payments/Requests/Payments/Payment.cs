namespace xperters.payments.Requests.Payments
{
    public class Payment
    {
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
    }
}