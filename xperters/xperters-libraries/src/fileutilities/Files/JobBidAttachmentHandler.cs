using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using xperters.domain;
using xperters.enums;
using xperters.fileutilities.Interfaces;

namespace xperters.fileutilities.Files
{
    public class JobBidAttachmentHandler : IAttachmentHandler<JobBidDto>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public JobBidAttachmentHandler(IBlobService blobService, ILoggerFactory loggerFactory)
        {
            _blobService = blobService;
            _logger = loggerFactory.CreateLogger<AttachmentHandler>();
        }

        public void ConvertFromWebFormToDto(JobBidDto jobDto, IFormFileCollection formFiles)
        {
            var attachments = new List<JobBidAttachmentDto>();

            const int bytes = 1024;
            const int megabytes = 1024;

            foreach (var file in formFiles)
            {
                const long bytesToRead = 10 * megabytes * bytes;

                if (file.Length > 0 && file.Length < bytesToRead)
                {
                    byte[] fileBytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    attachments.Add(new JobBidAttachmentDto
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        FileData = fileBytes,
                        MimeType = file.ContentType
                    });
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"file size is outside expected limit. size: {file.Length}. File size must be between 1 byte and 10MB");
                }
            }

            jobDto.JobBidAttachments = attachments;
        }

        public void StoreAttachmentsToBlob(JobBidDto jobBidDto)
        {
            var jobId = jobBidDto.JobId;
            var userId = jobBidDto.Job.UserId;
            var bidId = jobBidDto.Id;

            if (jobId == Guid.Empty || userId == Guid.Empty)
            {
                _logger.LogWarning("Attachment cannot be saved because jobid or userid is null");
                return;
            }
            if (jobBidDto.JobBidAttachments != null)
            {
                foreach (var attachment in jobBidDto.JobBidAttachments)
                {
                    attachment.LocalPath = $"{userId}/{jobId}/Bids/{bidId}/{Path.GetFileNameWithoutExtension(attachment.FileName)}{Path.GetExtension(attachment.FileName)}";
                    attachment.Uri = _blobService.AddToBlobForJobBidDto(Enums.FileFor.JobAttachments, attachment);
                }
            }
        }
    }
}
