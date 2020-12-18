using System;

namespace xperters.domain
{
  public class EmailAttachmentsDto:AttachmentDto
    {
        public Guid EmailAuditId { get; set; }
        public EmailAuditDto EmailAuditDto { get; set; }
    }
}
