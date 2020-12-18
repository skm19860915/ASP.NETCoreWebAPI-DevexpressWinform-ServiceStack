using System;
using System.Collections.Generic;
using System.Text;
using xperters.constants;
using xperters.domain;

namespace xperters.mockdata
{
    public class EmailAttachmentsMock
    {
        private static readonly List<EmailAttachmentsDto> _attachments;
        public static EmailAttachmentsDto emailAttachments1 { get; }
        public static EmailAttachmentsDto emailAttachments2 { get; }
        public static EmailAttachmentsDto emailAttachments3 { get; }
        public static EmailAttachmentsDto emailAttachments4 { get; }

        static EmailAttachmentsMock()
        {
            emailAttachments1 = new EmailAttachmentsDto
            {
                Uri = "Bolivia-APER.xlsx",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000001}"),
                CreatedDate = new DateTime(2018, 01, 01)
            };
            emailAttachments2 = new EmailAttachmentsDto
            {
                Uri = "CLASS.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000002}"),
                CreatedDate = new DateTime(2018, 01, 02)
            };
            emailAttachments3 = new EmailAttachmentsDto
            {
                Uri = "gre_research_validity_data.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000003}"),
                CreatedDate = new DateTime(2018, 01, 04)
            };
            emailAttachments4 = new EmailAttachmentsDto
            {
                Uri = "HandwashingWithAnanseBookCompressed.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000004}"),
                CreatedDate = new DateTime(2018, 01, 05)
            };
            _attachments = new List<EmailAttachmentsDto>();
            _attachments.AddRange(new[]
            {
                emailAttachments1,
                emailAttachments2,
                emailAttachments3,
                emailAttachments4,
            });
            foreach (var attachment in _attachments)
            {
                attachment.FileName = attachment.Uri;
            }

        }
        public static List<EmailAttachmentsDto> Get()
        {
            return _attachments;
        }

    }
}
