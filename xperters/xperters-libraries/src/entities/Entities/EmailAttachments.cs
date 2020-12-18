using System;

namespace xperters.entities.Entities
{
    public class EmailAttachments : Attachment
    {
        public Guid EmailAuditId { get; set; }
        public virtual EmailAudit EmailsAudit { get; set; }
    }
}
