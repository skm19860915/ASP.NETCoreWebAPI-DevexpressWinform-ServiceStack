using System;
using System.Collections.Generic;

namespace xperters.domain
{
   public class ContractChatSessionDto:BaseDto
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public List<ContractChatSessionUserDto> ContractChatSessionUsersDto { get; set; }
        public JobDto jobDto { get; set; }
        public List<ContractChatMessageDto> ContractChatMessagesDto { get; set; }
    }
}
