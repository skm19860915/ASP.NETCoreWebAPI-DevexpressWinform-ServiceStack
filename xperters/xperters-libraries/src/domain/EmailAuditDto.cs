using System;
using System.Collections.Generic;

namespace xperters.domain
{
   public  class EmailAuditDto:BaseDto
    {
        public Guid SenderId { get; set; }
        public string SenderEmailAddress { get; set; }
        public Guid ReceiverId { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string Content { get; set; }

        public List<EmailAttachmentsDto> EmailAttachmentsDto { get; set; }

    }
}
