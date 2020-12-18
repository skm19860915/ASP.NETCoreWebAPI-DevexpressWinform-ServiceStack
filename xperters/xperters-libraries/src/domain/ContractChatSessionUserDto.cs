using System;

namespace xperters.domain
{
  public class ContractChatSessionUserDto:BaseDto
    {
        public Guid ContractChatSessionId { get; set; }
        public Guid UserId { get; set; }
        public ContractChatSessionDto ContractChatSessionDto { get; set; }
        public UserDto UserDto { get; set; }
    }
}
