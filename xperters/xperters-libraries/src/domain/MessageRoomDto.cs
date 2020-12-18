using System;
using System.Collections.Generic;

namespace xperters.domain
{
  public class MessageRoomDto:BaseDto
    {
       
        public Guid JobId { get; set; }
        public JobDto jobDto { get; set; }
        public List<JobBidMessagesDto> jobBidMessages { get; set; }
    }
}
