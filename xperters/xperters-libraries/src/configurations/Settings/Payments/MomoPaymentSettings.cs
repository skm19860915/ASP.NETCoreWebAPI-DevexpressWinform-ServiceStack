namespace xperters.configurations.Settings.Payments
{
    public class MomoPaymentSettings
    {
        public string BaseUri { get; set; }
        public string Environment { get; set; }
        public MomoSettings Collection { get; set; }
        public MomoSettings Disbursement { get; set; }
        public MomoSettings Remittance { get; set; }
    }
}