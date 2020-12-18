using System;
using System.Collections.Generic;


namespace xperters.models
{
    public class MilestoneView : BaseView
    {
        
        public string Description { get; set; }
        public DateTime Due { get; set; }
        public decimal Amount { get; set; }
        public int MilestoneStatus { get; set; }
        public string Status { get; set; }
        public string FreelancerStatus { get; set; }
        public Guid ContractId { get; set; }
        public Guid CreatedId { get; set; }

        public List<MilestoneAttachmentView> MilestoneAttachments { get; set; }
    }
}
