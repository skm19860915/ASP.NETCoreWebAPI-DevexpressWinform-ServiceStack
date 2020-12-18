using System;

namespace xperters.domain
{
  public  class HiredJobDto:BaseDto
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public string Message { get; set; }
        public int messageType { get; set; }
        public Guid ContractChatSessionId { get; set; }
        public decimal Amount { get; set; }
        public int ContractStatus { get; set; }
    
}
}
