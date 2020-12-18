using System;

namespace xperters.entities.Entities
{
    public class JobAttachment: Attachment
    {
        public Guid JobId { get; set; }
        public virtual Job Job { get; set;}
    }
}
