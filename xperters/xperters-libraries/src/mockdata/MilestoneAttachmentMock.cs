using System;
using System.Collections.Generic;
using System.Text;
using xperters.constants;
using xperters.domain;

namespace xperters.mockdata
{
  public  class MilestoneAttachmentMock
    {
        private static readonly List<MilestoneAttachmentDto> _attachments;
        public static MilestoneAttachmentDto milestoneAttachment1 { get; }
        public static MilestoneAttachmentDto milestoneAttachment2{ get; }
        public static MilestoneAttachmentDto milestoneAttachment3{ get; }
        public static MilestoneAttachmentDto milestoneAttachment4{ get; }
        public static MilestoneAttachmentDto milestoneAttachment5{ get; }
        public static MilestoneAttachmentDto milestoneAttachment6{ get; }
        public static MilestoneAttachmentDto milestoneAttachment7{ get; }
        public static MilestoneAttachmentDto milestoneAttachment8{ get; }
        public static MilestoneAttachmentDto milestoneAttachment9{ get; }
        public static MilestoneAttachmentDto milestoneAttachment10 { get; }

        static MilestoneAttachmentMock()
        {
            milestoneAttachment1 = new MilestoneAttachmentDto
            {
                Uri = "Bolivia-APER.xlsx",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000001}"),
                CreatedDate = new DateTime(2018, 01, 01)
            };
            milestoneAttachment2 = new MilestoneAttachmentDto
            {
                Uri = "CLASS.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000002}"),
                CreatedDate = new DateTime(2018, 01, 02)
            };
            milestoneAttachment3 = new MilestoneAttachmentDto
            {
                Uri = "gre_research_validity_data.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000003}"),
                CreatedDate = new DateTime(2018, 01, 04)
            };
            milestoneAttachment4 = new MilestoneAttachmentDto
            {
                Uri = "HandwashingWithAnanseBookCompressed.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000004}"),
                CreatedDate = new DateTime(2018, 01, 05)
            };
            milestoneAttachment5 = new MilestoneAttachmentDto
            {
                Uri = "Lorem Ipsum.docx",
                MimeType = MimeTypeConstants.MimeTypeWord,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000005}"),
                CreatedDate = new DateTime(2018, 01, 06)
            };
            milestoneAttachment6 = new MilestoneAttachmentDto
            {
                Uri = "pdf-sample.pdf",
                MimeType = MimeTypeConstants.MimeTypePdf,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000006}"),
                CreatedDate = new DateTime(2018, 01, 07)
            };
            milestoneAttachment7 = new MilestoneAttachmentDto
            {
                Uri = "SampleXLSFile_904kb.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000007}"),
                CreatedDate = new DateTime(2018, 01, 08)
            };
            milestoneAttachment8 = new MilestoneAttachmentDto
            {
                Uri = "tests-example.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{40000000-0000-0000-0000-000000000008}"),
                CreatedDate = new DateTime(2018, 01, 09)
            };
            milestoneAttachment9 = new MilestoneAttachmentDto
            {
                Uri = "version6.doc",
                MimeType = MimeTypeConstants.MimeTypeWord,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000009}"),
                CreatedDate = new DateTime(2018, 01, 10)
            };
            milestoneAttachment10 = new MilestoneAttachmentDto
            {
                Uri = "world_bank_data_catalog.xls",
                MimeType = MimeTypeConstants.MimeTypeExcel,
                Id = Guid.Parse("{60000000-0000-0000-0000-000000000010}"),
                CreatedDate = new DateTime(2018, 01, 11)
            };

            _attachments = new List<MilestoneAttachmentDto>();

            // add them to a list
            _attachments.AddRange(new[]
            {
                milestoneAttachment1,
                milestoneAttachment2,
                milestoneAttachment3,
                milestoneAttachment4,
                milestoneAttachment5,
                milestoneAttachment6,
                milestoneAttachment7,
                milestoneAttachment8,
                milestoneAttachment9,
                milestoneAttachment10,
            });

            foreach (var attachment in _attachments)
            {
                attachment.FileName = attachment.Uri;
            }
        }
            public static List<MilestoneAttachmentDto> Get()
            {
                return _attachments;
            }
        
    }
}
