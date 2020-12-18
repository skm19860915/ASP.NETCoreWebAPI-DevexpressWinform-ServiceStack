using System;
using System.Collections.Generic;

namespace xperters.models
{
  public class JobBidView : BaseView
    {
        public string Message { get; set; }
        public Guid JobId { get; set; }
        public Guid FreelancerUserId { get; set; }
        public JobView Job { get; set; }
        public List<JobBidAttachmentView> JobBidAttachments { get; set; }
        public List<JobBidMessagesView> JobBidMessages { get; set; }
        public decimal BidAmount { get; set; }
        public UserView User { get; set; }
        public int BidStatus { get; set; }
    
    }
}
