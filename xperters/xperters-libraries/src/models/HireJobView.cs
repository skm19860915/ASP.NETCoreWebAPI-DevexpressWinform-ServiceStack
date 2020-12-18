using System;

namespace xperters.models
{
   public class HireJobView : BaseView
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public string Message { get; set; }
        public int messageType { get; set; }
        public Guid ContractChatSessionsId { get; set; }
        public decimal Amount { get; set; }
       
        public int BidStatus { get; set; }
        public int ContractStatus { get; set; }
    }
}
