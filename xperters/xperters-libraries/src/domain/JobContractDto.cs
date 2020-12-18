using System;
using System.Collections.Generic;

namespace xperters.domain
{
   public class JobContractDto:BaseDto
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public decimal Amount { get; set; }
        public int ContractStatus { get; set; }
        public List<MilestoneDto> Milestones { get; set; }

        public JobDto Job { get; set; }
        public  UserDto Freelancer { get; set; }
    }
}
