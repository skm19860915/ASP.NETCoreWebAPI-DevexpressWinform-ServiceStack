using System;

namespace xperters.domain
{
  public  class MilestoneAttachmentDto:AttachmentDto
    {
        public Guid MilestoneId { get; set; }
        public MilestoneDto Milestone { get; set; }
    }
}
