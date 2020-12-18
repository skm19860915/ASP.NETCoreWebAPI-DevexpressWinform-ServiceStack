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
    public class JobAttachmentReader : ITypeConverter<JobAttachment, JobAttachmentDto>
    {
        private readonly IBlobService _blobService;

        public JobAttachmentReader(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public JobAttachmentDto Convert(JobAttachment source, JobAttachmentDto destination, ResolutionContext context)
        {

            if (source == null)
            {
                return null;
            }

            var uri = source.Uri;
            var dto = new JobAttachmentDto
            {
                Id = source.Id,
                JobId = source.JobId,
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
