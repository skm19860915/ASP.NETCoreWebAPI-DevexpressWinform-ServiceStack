using System;
using System.Collections.Generic;
using System.Text;
using xperters.constants;
using xperters.domain;

namespace xperters.mockdata
{
 public  class JobBidAttachments
    {
        
            private static readonly List<JobBidAttachmentDto> _attachments;
            public static JobBidAttachmentDto JobBidAttachment1 { get; }
            public static JobBidAttachmentDto JobBidAttachment2 { get; }
            public static JobBidAttachmentDto JobBidAttachment3 { get; }
            public static JobBidAttachmentDto JobBidAttachment4 { get; }
            public static JobBidAttachmentDto JobBidAttachment5 { get; }
            public static JobBidAttachmentDto JobBidAttachment6 { get; }
            public static JobBidAttachmentDto JobBidAttachment7 { get; }
            public static JobBidAttachmentDto JobBidAttachment8 { get; }
            public static JobBidAttachmentDto JobBidAttachment9 { get; }
            public static JobBidAttachmentDto JobBidAttachment10 { get; }

            static JobBidAttachments()
            {
            JobBidAttachment1 = new JobBidAttachmentDto
                {
                    Uri = "Bolivia-APER.xlsx",
                    MimeType = MimeTypeConstants.MimeTypeExcel,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000001}"),
                    CreatedDate = new DateTime(2018, 01, 01)
                };
            JobBidAttachment2 = new JobBidAttachmentDto
                {
                    Uri = "CLASS.xls",
                    MimeType = MimeTypeConstants.MimeTypeExcel,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000002}"),
                    CreatedDate = new DateTime(2018, 01, 02)
                };
            JobBidAttachment3 = new JobBidAttachmentDto
                {
                    Uri = "gre_research_validity_data.pdf",
                    MimeType = MimeTypeConstants.MimeTypePdf,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000003}"),
                    CreatedDate = new DateTime(2018, 01, 04)
                };
            JobBidAttachment4 = new JobBidAttachmentDto
                {
                    Uri = "HandwashingWithAnanseBookCompressed.pdf",
                    MimeType = MimeTypeConstants.MimeTypePdf,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000004}"),
                    CreatedDate = new DateTime(2018, 01, 05)
                };
            JobBidAttachment5 = new JobBidAttachmentDto
                {
                    Uri = "Lorem Ipsum.docx",
                    MimeType = MimeTypeConstants.MimeTypeWord,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000005}"),
                    CreatedDate = new DateTime(2018, 01, 06)
                };
            JobBidAttachment6 = new JobBidAttachmentDto
                {
                    Uri = "pdf-sample.pdf",
                    MimeType = MimeTypeConstants.MimeTypePdf,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000006}"),
                    CreatedDate = new DateTime(2018, 01, 07)
                };
            JobBidAttachment7 = new JobBidAttachmentDto
                {
                    Uri = "SampleXLSFile_904kb.xls",
                    MimeType = MimeTypeConstants.MimeTypeExcel,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000007}"),
                    CreatedDate = new DateTime(2018, 01, 08)
                };
            JobBidAttachment8 = new JobBidAttachmentDto
                {
                    Uri = "tests-example.xls",
                    MimeType = MimeTypeConstants.MimeTypeExcel,
                    Id = Guid.Parse("{40000000-0000-0000-0000-000000000008}"),
                    CreatedDate = new DateTime(2018, 01, 09)
                };
            JobBidAttachment9 = new JobBidAttachmentDto
                {
                    Uri = "version6.doc",
                    MimeType = MimeTypeConstants.MimeTypeWord,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000009}"),
                    CreatedDate = new DateTime(2018, 01, 10)
                };
            JobBidAttachment10 = new JobBidAttachmentDto
                {
                    Uri = "world_bank_data_catalog.xls",
                    MimeType = MimeTypeConstants.MimeTypeExcel,
                    Id = Guid.Parse("{60000000-0000-0000-0000-000000000010}"),
                    CreatedDate = new DateTime(2018, 01, 11)
                };

                _attachments = new List<JobBidAttachmentDto>();

                // add them to a list
                _attachments.AddRange(new[]
                {
                JobBidAttachment1,
                JobBidAttachment2,
                JobBidAttachment3,
                JobBidAttachment4,
                JobBidAttachment5,
                JobBidAttachment6,
                JobBidAttachment7,
                JobBidAttachment8,
                JobBidAttachment9,
                JobBidAttachment10
            });

                foreach (var attachment in _attachments)
                {
                    attachment.FileName = attachment.Uri;
                }

            }

            public static List<JobBidAttachmentDto> Get()
            {
                return _attachments;
            }
        }
    }



