using System;
using System.Collections.Generic;

namespace xperters.models
{
  public class MessageRoomView:BaseView
    {
        public Guid JobId { get; set; }
        public JobView jobView { get; set; }
        public List<JobBidMessagesView> jobBidMessages { get; set; }

    }
}
