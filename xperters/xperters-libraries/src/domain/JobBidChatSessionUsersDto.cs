using System;

namespace xperters.domain
{
    public class JobBidChatSessionUsersDto : BaseDto
    {
        public Guid JobBidChatSessionId { get; set; }        
        public Guid UserId { get; set; }
        public JobBidChatSessionDto JobBidChatSession { get; set; }
        public UserDto User { get; set; }
    }
}
