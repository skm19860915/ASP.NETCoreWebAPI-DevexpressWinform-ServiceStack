using System;

namespace xperters.domain
{
  public class ContractChatMessageDto:BaseDto
    {
        public Guid SenderId { get; set; }
        public Guid ContractChatSessionId { get; set; }
        public string Message { get; set; }
        public bool MsgType { get; set; }
        public UserDto User { get; set; }
        public ContractChatSessionDto ContractChatSession { get; set; }
    }
}
