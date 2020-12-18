using System;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public class PaymentIncomingViewModel
    {
        public string MilestoneDescription { get; set; }
        public string UserName { get; set; }
        public string Currency { get; set; }
        public string PayerStatus { get; set; }
        public string ResponseMessage { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public decimal Amount { get; set; }
        public decimal FeeFlat { get; set; }
        public decimal FeePercent { get; set; }
        public decimal FeeTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid Id { get; set; }
    }
}
