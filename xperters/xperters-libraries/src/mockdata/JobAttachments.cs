using System;
using System.Collections.Generic;
using xperters.constants;
using xperters.domain;

namespace xperters.mockdata
{
    public static class JobAttachments
    {
        private static readonly List<JobAttachmentDto> _attachments;
        public static JobAttachmentDto JobAttachment1 { get; }
        public static JobAttachmentDto JobAttachment2 { get; }
        public static JobAttachmentDto JobAttachment3 { get; }
        public static JobAttachmentDto JobAttachment4 { get; }
        public static JobAttachmentDto JobAttachment5 { get; }
        public static JobAttachmentDto JobAttachment6 { get; }
        public static JobAttachmentDto JobAttachment7 { get; }
        public static JobAttachmentDto JobAttachment8 { get; }
        public static JobAttachmentDto JobAttachment9 { get; }
        public static JobAttachmentDto JobAttachment10 { get;}

        static JobAttachments()
        {
            JobAttachment1 = new JobAttachmentDto
            {
                Uri = "Bolivia-APER.xlsx",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000001}"),
                CreatedDate = new DateTime(2018, 01, 01)
            };
            JobAttachment2 = new JobAttachmentDto
            {
                Uri = "CLASS.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000002}"),
                CreatedDate = new DateTime(2018, 01, 02)
            };
            JobAttachment3 = new JobAttachmentDto
            {
                Uri = "gre_research_validity_data.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000003}"),
                CreatedDate = new DateTime(2018, 01, 04)
            };
            JobAttachment4 = new JobAttachmentDto
            {
                Uri = "HandwashingWithAnanseBookCompressed.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000004}"),
                CreatedDate = new DateTime(2018, 01, 05)
            };
            JobAttachment5 = new JobAttachmentDto
            {
                Uri = "Lorem Ipsum.docx",
                MimeType = MimeTypeConstants.MimeTypeWord,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000005}"),
                CreatedDate = new DateTime(2018, 01, 06)
            };
            JobAttachment6 = new JobAttachmentDto
            {
                Uri = "pdf-sample.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000006}"),
                CreatedDate = new DateTime(2018, 01, 07)
            };
            JobAttachment7 = new JobAttachmentDto
            {
                Uri = "SampleXLSFile_904kb.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000007}"),
                CreatedDate = new DateTime(2018, 01, 08)
            };
            JobAttachment8 = new JobAttachmentDto
            {
                Uri = "tests-example.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000008}"),
                CreatedDate = new DateTime(2018, 01, 09)
            };
            JobAttachment9 = new JobAttachmentDto
            {
                Uri = "version6.doc",
                MimeType = MimeTypeConstants.MimeTypeWord,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000009}"),
                CreatedDate = new DateTime(2018, 01, 10)
            };
            JobAttachment10 = new JobAttachmentDto
            {
                Uri = "world_bank_data_catalog.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000010}"),
                CreatedDate = new DateTime(2018, 01, 11)
            };

            _attachments = new List<JobAttachmentDto>();

            // add them to a list
            _attachments.AddRange(new[]
            {
                JobAttachment1,
                JobAttachment2,
                JobAttachment3,
                JobAttachment4,
                JobAttachment5,
                JobAttachment6,
                JobAttachment7,
                JobAttachment8,
                JobAttachment9,
                JobAttachment10
            });

            foreach (var attachment in _attachments)
            {
                attachment.FileName = attachment.Uri;
            }

        }

        public static List<JobAttachmentDto> Get()
        {
            return _attachments;
        }
    }
}
