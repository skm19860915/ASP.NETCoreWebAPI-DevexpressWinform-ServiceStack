using System;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public class PaymentOutgoingViewModel
    {
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceOld { get; set; }
        public decimal BalanceNew { get; set; }
        public string Currency { get; set; }
        public string PayerStatus { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string PaymentTransactionType { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid Id { get; set; }
    }
}
