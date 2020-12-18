using System;

namespace xperters.models
{
   public  class ContractMilestoneFundView:BaseView
    {
        public Guid UserId { get; set; }
        public Guid MilestoneId { get; set; }
        public int FundStatus { get; set; }
        public decimal Amount { get; set; }

        public UserView User { get; set; }
        public MilestoneView Milestone { get; set; }
    }
}
