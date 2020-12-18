using System;
using System.Collections.Generic;

namespace xperters.entities.Entities
{
    public class EmailAudit : BaseEntity
    {
        public Guid SenderId { get; set; }
        public string SenderEmailAddress { get; set; }
        public Guid ReceiverId { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string Content { get; set; }

        public User User { get; set; }
        public ICollection<EmailAttachments> EmailAttachments { get; set; }
    }
}
