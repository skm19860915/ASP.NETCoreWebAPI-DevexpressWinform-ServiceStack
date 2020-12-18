using System;
using System.Collections.Generic;

namespace xperters.domain
{
  public class JobBidChatSessionDto : BaseDto
    {
       
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public JobDto Job { get; set; }
        public UserDto Freelancer { get; set; }
        public UserDto Client { get; set; }
        public List<JobBidChatMessageDto> JobBidChatMessages { get; set; }

    }
}
