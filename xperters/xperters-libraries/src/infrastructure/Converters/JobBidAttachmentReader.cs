using AutoMapper;
using xperters.domain;
using xperters.entities.Entities;
using xperters.enums;
using xperters.fileutilities.Interfaces;

namespace xperters.infrastructure.Converters
{
    /// <summary>
    /// Use the uri that is in the job to fetch the file data from blobservice
    /// and use it to populate the jobdto
    /// </summary>
    public class JobBidAttachmentReader : ITypeConverter<JobBidAttachment, JobBidAttachmentDto>
    {
        private readonly IBlobService _blobService;

        public JobBidAttachmentReader(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public JobBidAttachmentDto Convert(JobBidAttachment source, JobBidAttachmentDto destination, ResolutionContext context)
        {

            if (source == null)
            {
                return null;
            }

            var uri = source.Uri;
            var dto = new JobBidAttachmentDto
            {
                Id = source.Id,
                JobBidId = source.JobBidId,
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
