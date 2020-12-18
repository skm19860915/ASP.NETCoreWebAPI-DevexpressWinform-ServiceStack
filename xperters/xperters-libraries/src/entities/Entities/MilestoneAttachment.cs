using System;

namespace xperters.entities.Entities
{
    public class MilestoneAttachment : Attachment
    {
        public Guid MilestoneId { get; set; }
        public virtual Milestone Milestone{ get; set; }
    }
}
