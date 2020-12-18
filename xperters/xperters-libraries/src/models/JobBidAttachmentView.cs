using System;

namespace xperters.models
{
    public class JobBidAttachmentView : AttachmentView
    {
        public Guid JobBidId { get; set; }
        public JobBidView JobBid { get; set; }
    }
}
