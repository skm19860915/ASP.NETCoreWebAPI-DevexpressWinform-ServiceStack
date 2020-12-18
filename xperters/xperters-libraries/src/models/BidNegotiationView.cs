using System;
using static xperters.enums.Enums;

namespace xperters.models
{
    public class BidNegotiationView : BaseView
    {

        public Guid JobId { get; set; }
        public Guid ClientId { get; set; }
        public Guid FreelancerId { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public Guid JobBidChatSessionId { get; set; }
        public int BidStatus { get; set; }
        public decimal BidAmount { get; set; }

        public SenderType SenderType { get; set; }
    }

   
}
