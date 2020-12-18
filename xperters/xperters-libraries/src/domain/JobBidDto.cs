using System;
using System.Collections.Generic;

namespace xperters.domain
{
   public class JobBidDto : BaseDto
    {
        public string Message { get; set; }
        public Guid JobId { get; set; }
        public Guid FreelancerUserId { get; set; }
        public JobDto Job { get; set; }
        public List<JobBidAttachmentDto> JobBidAttachments { get; set; }
        public List<JobBidChatMessageDto> JobBidMessages { get; set; }
        public decimal BidAmount { get; set; }
        public UserDto User { get; set; }
        public int BidStatus { get; set; }


    }
}
