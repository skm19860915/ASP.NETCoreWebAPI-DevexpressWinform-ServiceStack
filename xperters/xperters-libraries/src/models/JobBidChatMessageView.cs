using System;
using static xperters.enums.Enums;

namespace xperters.models
{
    public class JobBidChatMessageView : BaseView
    {
        public Guid JobId { get; set; }
        public Guid SenderId { get; set; }
        public Guid JobBidChatSessionId { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }

        public SenderType SenderType { get; set; }
    }
}
