using System;

namespace xperters.domain
{
    public class MilestonePaymentDto 
    {
        public Guid MilestoneId { get; set; }
        public string MilestoneDescription { get; set; }
        public DateTime MilestoneDueDate { get; set; }
        public string MilestoneStatus { get; set; }
        public DateTime MilestoneCreated { get; set; }
        public DateTime RequestPayerCreated { get; set; }
        public string JobTitle { get; set; }
        public decimal MilestoneAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public int? PaymentServiceCheckCount { get; set; }        
    }
}
