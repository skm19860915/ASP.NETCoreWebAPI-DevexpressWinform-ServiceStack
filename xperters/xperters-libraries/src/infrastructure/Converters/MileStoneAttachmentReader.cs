using AutoMapper;
using xperters.domain;
using xperters.entities.Entities;
using xperters.enums;
using xperters.fileutilities.Interfaces;

namespace xperters.infrastructure.Converters
{
    public class MilestoneAttachmentReader : ITypeConverter<MilestoneAttachment, MilestoneAttachmentDto>
    {
        private readonly IBlobService _blobService;

        public MilestoneAttachmentReader(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public MilestoneAttachmentDto Convert(MilestoneAttachment source, MilestoneAttachmentDto destination, ResolutionContext context)
        {

            if (source == null)
            {
                return null;
            }

            var uri = source.Uri;
            var dto = new MilestoneAttachmentDto
            {
                Id = source.Id,
                MilestoneId = source.MilestoneId,
                CreatedDate = source.CreatedDate,
                FileSize = source.FileSize,
                FileName = source.FileName,
                LocalPath = source.LocalPath,
                MimeType = source.MimeType,
                Uri = uri,
                FileData = null
            };
            // check the url 
            if (!string.IsNullOrEmpty(uri))
            {
                var data = _blobService.GetBytesFromBlobStorage(Enums.FileFor.JobAttachments, uri);
                dto.FileData = data;
            }
            if (destination != null)
            {
                destination.FileData = dto.FileData;
            }

            return dto;
        
    }
    }
}
