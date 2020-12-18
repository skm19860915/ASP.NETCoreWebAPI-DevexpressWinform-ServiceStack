using System;

namespace xperters.domain
{
    public class MilestoneDetailDto
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
