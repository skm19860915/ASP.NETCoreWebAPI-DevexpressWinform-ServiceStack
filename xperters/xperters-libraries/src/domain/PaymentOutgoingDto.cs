using System;

namespace xperters.domain
{
    public class PaymentOutgoingDto : BaseDto
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
        public DateTime Created { get; set; }
    }
}
