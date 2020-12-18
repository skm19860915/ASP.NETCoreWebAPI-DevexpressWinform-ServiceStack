using System;

namespace xperters.entities.Entities
{
    public class JobBidAttachment:Attachment
    {
        public Guid JobBidId { get; set; }

        public virtual JobBid JobBid { get; set; }
      
    }
}
