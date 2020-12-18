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
    public class MilestoneAttachmentHandler : IAttachmentHandler<MilestoneDto>
    {
        private readonly IBlobService _blobService;
        private readonly ILogger _logger;


        public MilestoneAttachmentHandler(IBlobService blobService, ILoggerFactory loggerFactory)
        {
            _blobService = blobService;
            _logger = loggerFactory.CreateLogger<AttachmentHandler>();
        }
        

        public void ConvertFromWebFormToDto(MilestoneDto dto, IFormFileCollection formFiles)
        {
            var attachments = new List<MilestoneAttachmentDto>();

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

                    attachments.Add(new MilestoneAttachmentDto
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

            dto.MilestoneAttachments = attachments;
        }

        public void StoreAttachmentsToBlob(MilestoneDto dto)
        {
            var milestoneId = dto.Id;
            var userId = dto.CreatedId;

            if (milestoneId == Guid.Empty || userId == Guid.Empty)
            {
                _logger.LogWarning("Attachment cannot be saved because jobid or userid is null");
                return;
            }
            if (dto.MilestoneAttachments != null)
            {
                foreach (var attachment in dto.MilestoneAttachments)
                {
                    attachment.LocalPath = $"{userId}/Milestone/{milestoneId}/{Path.GetFileNameWithoutExtension(attachment.FileName)}{Path.GetExtension(attachment.FileName)}";
                    attachment.Uri = _blobService.AddToBlobForMilestoneDto(Enums.FileFor.JobAttachments, attachment);
                }
            }
        }








    }
}
