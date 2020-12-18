using System;
using static xperters.enums.Enums;

namespace xperters.domain
{
    public class JobBidChatMessageDto : BaseDto
    {
        public Guid SenderId { get; set; }
        public Guid JobBidChatSessionId { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public UserDto Sender { get; set; }
        public JobBidChatSessionDto JobBidChatSession { get; set; }

        //public SenderType SenderType { get; set; }
    }
}
