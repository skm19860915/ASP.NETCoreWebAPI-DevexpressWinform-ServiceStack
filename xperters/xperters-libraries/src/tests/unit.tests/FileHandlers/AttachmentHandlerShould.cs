using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;
using xperters.domain;
using xperters.entities;
using xperters.enums;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.mockdata;

namespace xperters.unit.tests.FileHandlers
{
    public class AttachmentHandlerShould : BaseUnitTests
    {

        private readonly Mock<IBlobService> _blobService;
        private readonly Mock<ILoggerFactory> _loggerFactory;

        public AttachmentHandlerShould()
        {
            _blobService = new Mock<IBlobService>();
            _loggerFactory = new Mock<ILoggerFactory>();

        }

        [Fact]
        public void ConvertFromWebFormToDto_ReturnsSingleAttachment()
        {
            var collection = CreateFormFileCollection();
            var handler = new AttachmentHandler(_blobService.Object, _loggerFactory.Object);
            var jobDto = CreateNewJob();

            Assert.Empty(jobDto.JobAttachments);

            handler.ConvertFromWebFormToDto(jobDto, collection);

            Assert.NotNull(jobDto.JobAttachments);
            Assert.True(jobDto.JobAttachments.Any());
            Assert.Single(jobDto.JobAttachments);
        }

        [Fact]
        public void ConvertFromWebFormToDto_ReturnsNoAttachment()
        {
            var collection = new FormFileCollection();
            var handler = new AttachmentHandler(_blobService.Object, _loggerFactory.Object);
            var jobDto = CreateNewJob();

            Assert.Empty(jobDto.JobAttachments);

            handler.ConvertFromWebFormToDto(jobDto, collection);

            Assert.Empty(jobDto.JobAttachments);
            Assert.False(jobDto.JobAttachments.Any());
        }

        [Fact]
        public void AddsAttachmentsToService_WhenThereAreItems()
        {
            var list = new List<JobAttachmentDto>();

            _blobService
                .Setup(x => x.AddToBlobForJobDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<JobAttachmentDto>()))
                .Callback<Enums.FileFor, JobAttachmentDto>(
                (f, j) =>
                {
                    list.Add(j);
                })
                .Returns<Enums.FileFor, JobAttachmentDto>((a, b) => b.FileName);


            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<AttachmentHandlerShould>();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<XpertersContext>(options=>options.UseInMemoryDatabase("AddsAttachments"))
                .BuildServiceProvider();
            var context = serviceProvider.GetService<XpertersContext>();
            var dataBuilder = new DataBuilder(context, Mapper, logger);
            dataBuilder.InitializeFileDataForMocks();

            Assert.True(dataBuilder.MockJobDtos.Any());

            var jobDto = Mapper.Map<JobDto>(dataBuilder.MockJobDtos.First());
            var mockAttachmentDtos = JobAttachments.Get().Where(x => x.JobId == jobDto.Id).ToList();
            jobDto.JobAttachments = mockAttachmentDtos;

            var handler = new AttachmentHandler(_blobService.Object, _loggerFactory.Object);

            handler.StoreAttachmentsToBlob(jobDto);
            var jobAttachmentsCount = jobDto.JobAttachments.Count;

            Assert.True(list.Any());
            Assert.Equal(jobAttachmentsCount, list.Count);
            Assert.Equal(mockAttachmentDtos[0].FileName, list[0].FileName);
            Assert.Equal(mockAttachmentDtos[1].FileName, list[1].FileName);
            Assert.Equal(mockAttachmentDtos[2].FileName, list[2].FileName);
            Assert.Equal(mockAttachmentDtos[3].FileName, list[3].FileName);

            _blobService.Verify(x => x.AddToBlobForJobDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<JobAttachmentDto>()), Times.Exactly(jobAttachmentsCount));
        }

        [Fact]
        public void AddsNothingToService_WhenNoItems()
        {
            var list = new List<JobAttachmentDto>();

            _blobService
                .Setup(x => x.AddToBlobForJobDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<JobAttachmentDto>()))
                .Callback<Enums.FileFor, JobAttachmentDto>(
                (f, j) =>
                {
                    list.Add(j);
                });


            var jobDto = new JobDto
            {
                JobAttachments = new List<JobAttachmentDto>(),
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                JobTitle = "Test jobs",
                Description = "Description1"
            };

            var handler = new AttachmentHandler(_blobService.Object, _loggerFactory.Object);

            handler.StoreAttachmentsToBlob(jobDto);
            var jobAttachmentsCount = jobDto.JobAttachments.Count;

            Assert.False(list.Any());
            Assert.Equal(jobAttachmentsCount, list.Count);

            _blobService.Verify(x => x.AddToBlobForJobDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<JobAttachmentDto>()), Times.Never);
        }

        private JobDto CreateNewJob()
        {
            var jobDto = new JobDto
            {
                Id = Jobs.Job1.Id,
                CreatedDate = Jobs.Job1.CreatedDate,
                ModifiedDate = Jobs.Job1.ModifiedDate,
                JobAttachments = new List<JobAttachmentDto>()
            };
            return jobDto;
        }
    }
}
