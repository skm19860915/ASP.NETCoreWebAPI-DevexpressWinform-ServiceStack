using System;

namespace xperters.domain
{
  public class JobBidAttachmentDto: AttachmentDto
    {
        public Guid JobBidId { get; set; }

        public JobBidDto JobBid { get; set; }
    }
}
