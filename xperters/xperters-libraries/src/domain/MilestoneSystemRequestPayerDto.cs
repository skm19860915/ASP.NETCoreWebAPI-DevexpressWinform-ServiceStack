using System;

namespace xperters.domain
{
    public class MilestoneSystemRequestPayerDto : BaseDto
    {
        public Guid MilestoneId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public int PayerStatusId { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsActive { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public DateTime? CompletedDate { get; set; }
        public MilestoneDto Milestone { get; set; }
        public RequestPayerStatusDto PayerStatus { get; set; }
    }
}
