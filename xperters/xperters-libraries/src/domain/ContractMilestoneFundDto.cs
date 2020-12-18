using System;

namespace xperters.domain
{
  public class ContractMilestoneFundDto :BaseDto
    {
        public Guid UserId { get; set; }
        public Guid MilestoneId { get; set; }
        public int FundStatus { get; set; }
        public decimal Amount { get; set; }

        public UserDto User { get; set; }
        public MilestoneDto Milestone { get; set; }

    }
}
