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
    public class AttachmentHandler : IAttachmentHandler<JobDto>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;

        public AttachmentHandler(IBlobService blobService, ILoggerFactory loggerFactory)
        {
            _blobService = blobService;
            _logger = loggerFactory.CreateLogger<AttachmentHandler>();
        }

        public void ConvertFromWebFormToDto(JobDto jobDto, IFormFileCollection formFiles)
        {
            var jobId = jobDto.Id;

            var attachments = new List<JobAttachmentDto>();

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

                    attachments.Add(new JobAttachmentDto
                    {
                        FileName = file.FileName,
                        FileSize = file.Length,
                        JobId = jobId,
                        FileData = fileBytes,
                        MimeType = file.ContentType
                    });
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"file size is outside expected limit. size: {file.Length}. File size must be between 1 byte and 10MB.");
                }
            }

            jobDto.JobAttachments = attachments;
        }

        public void StoreAttachmentsToBlob(JobDto jobDto)
        {
            var jobId = jobDto.Id;
            var userId = jobDto.UserId;

            if (userId == Guid.Empty || jobId == Guid.Empty)
            {
                _logger.LogWarning("Attachment cannot be saved because jobid or userid is null");
                return;
            }

            foreach (var attachment in jobDto.JobAttachments)
            {
                attachment.JobId = jobId;
                attachment.LocalPath = $"{userId}/{jobId}/{Path.GetFileNameWithoutExtension(attachment.FileName)}{Path.GetExtension(attachment.FileName)}";
                attachment.Uri = _blobService.AddToBlobForJobDto(Enums.FileFor.JobAttachments, attachment);
            }
        }
    }
}
