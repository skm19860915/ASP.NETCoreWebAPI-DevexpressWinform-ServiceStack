using System;

namespace xperters.domain
{
    public class MilestoneRequestPayerDto : BaseDto
    {
        public Guid MilestoneId { get; set; }
        public Guid ClientId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public decimal FeeFlat { get; set; }
        public decimal FeeTotal { get; set; }
        public decimal FeePercent { get; set; }
        public decimal TotalAmount { get; set; }        
        public int PayerStatusId { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? CompletedDate { get; set; }
        public MilestoneDto Milestone { get; set; }
        public RequestPayerStatusDto PayerStatus { get; set; }
    }
}
