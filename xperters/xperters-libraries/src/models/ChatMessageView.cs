using System;
using System.Collections.Generic;


namespace xperters.models
{
    public class ChatMessageView : BaseView
    {
        public int MessageType { get; set; }
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }

        public Guid ClientId { get; set; }
    }
}
