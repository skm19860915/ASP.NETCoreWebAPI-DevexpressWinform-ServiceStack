using System;

namespace xperters.domain
{
    public class JobAttachmentDto: AttachmentDto
    {
        public Guid JobId { get; set; }
        public JobDto Job { get; set; }
    }
}
