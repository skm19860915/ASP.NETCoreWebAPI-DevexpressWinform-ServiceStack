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
using xperters.infrastructure.Converters;
using xperters.entities.Entities;

namespace xperters.unit.tests.FileHandlers
{
   public class MilestoneAttachmentHandlerShould : BaseUnitTests
    {
        private readonly Mock<IBlobService> _blobService;
        private readonly Mock<ILoggerFactory> _loggerFactory;
        //private readonly Mock<MilestoneAttachmentReader> _milestoneAttachmentReader;


        //public MilestoneAttachmentHandlerShould(Mock<MilestoneAttachmentReader> milestoneAttachmentReader)
        public MilestoneAttachmentHandlerShould()
        {
            //_milestoneAttachmentReader = milestoneAttachmentReader;
            _blobService = new Mock<IBlobService>();
            _loggerFactory = new Mock<ILoggerFactory>();

        }

        //[Fact(Skip = "Errors found in these tests")]
        [Fact]
        public void ConvertFromWebFormToDto_ReturnsSingleAttachment()
        {
            var collection = CreateFormFileCollection();
            var handler = new MilestoneAttachmentHandler(_blobService.Object, _loggerFactory.Object);
            var milestone = CreateNewJob();

            Assert.Empty(milestone.MilestoneAttachments);

            handler.ConvertFromWebFormToDto(milestone, collection);

            Assert.NotNull(milestone.MilestoneAttachments);
            Assert.True(milestone.MilestoneAttachments.Any());
            Assert.Single(milestone.MilestoneAttachments);
        }

        //[Fact(Skip = "Errors found in these tests")]
        [Fact]
        public void ConvertFromWebFormToDto_ReturnsNoAttachment()
        {
            var collection = new FormFileCollection();
            var handler = new MilestoneAttachmentHandler(_blobService.Object, _loggerFactory.Object);
            var milestone = CreateNewJob();

            Assert.Empty(milestone.MilestoneAttachments);

            handler.ConvertFromWebFormToDto(milestone, collection);

            Assert.Empty(milestone.MilestoneAttachments);
            Assert.False(milestone.MilestoneAttachments.Any());
        }

        [Fact (Skip = "Fails intermittently")]
        public void AddsAttachmentsToService_WhenThereAreItems()
        {
            var list = new List<MilestoneAttachmentDto>();

            _blobService
                .Setup(x => x.AddToBlobForMilestoneDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<MilestoneAttachmentDto>()))
                .Callback<Enums.FileFor, MilestoneAttachmentDto>(
                (f, j) =>
                {
                    list.Add(j);
                })
                .Returns<Enums.FileFor, MilestoneAttachmentDto>((a, b) => b.FileName);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<MilestoneAttachmentHandlerShould>();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<XpertersContext>(options => options.UseInMemoryDatabase("AddsAttachments"))
                .AddSingleton(_blobService.Object)
                .AddTransient<IAttachmentHandler<JobDto>, AttachmentHandler>()
                .AddTransient<IAttachmentHandler<JobBidDto>, JobBidAttachmentHandler>()                
                .AddTransient<IAttachmentHandler<MilestoneDto>, MilestoneAttachmentHandler>()                
                .BuildServiceProvider();
            var context = serviceProvider.GetService<XpertersContext>();
            var dataBuilder = new DataBuilder(context, Mapper, logger);
          
            // seed database
            var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
            var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
            var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();
            
            dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
            dataBuilder.InitializeFileDataForMocks();
            var milestonesEmptyId = dataBuilder.Milestones.Any(x=>x.Id == Guid.Empty);

            Assert.True(dataBuilder.Milestones.Any());
            Assert.False(milestonesEmptyId);            
            
            var milestoneDto = Mapper.Map<MilestoneDto>(dataBuilder.Milestones.First());
            var mockAttachmentDtos = MilestoneAttachmentMock.Get().Where(x => x.MilestoneId == milestoneDto.Id).ToList();
            milestoneDto.MilestoneAttachments = mockAttachmentDtos;

            var handler = new MilestoneAttachmentHandler(_blobService.Object, _loggerFactory.Object);

            handler.StoreAttachmentsToBlob(milestoneDto);

            Assert.True(list.Any());
            Assert.Equal(mockAttachmentDtos[0].FileName, list[0].FileName);
            Assert.Equal(mockAttachmentDtos[1].FileName, list[1].FileName);

            _blobService.Verify(x => x.AddToBlobForMilestoneDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<MilestoneAttachmentDto>()), Times.AtLeast(10));
        }

        //[Fact(Skip = "Errors found in these tests")]
        [Fact]
        public void AddsNothingToService_WhenNoItems()
        {
            var list = new List<MilestoneAttachmentDto>();

            _blobService
                .Setup(x => x.AddToBlobForMilestoneDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<MilestoneAttachmentDto>()))
                .Callback<Enums.FileFor, MilestoneAttachmentDto>(
                (f, j) =>
                {
                    list.Add(j);
                });


            var milestoneDto = new MilestoneDto
            {
                MilestoneAttachments = new List<MilestoneAttachmentDto>(),
                Id = Guid.NewGuid(),
                CreatedId = Guid.NewGuid(),
                Amount =1234,
                Description = "Description1",
                ContractId= Guid.NewGuid(),
                MilestoneStatus = Enums.MilestoneStatus.AddFunds.GetEnumValue(),
                DueDate = Convert.ToDateTime("2019-04-10")
            };

            var handler = new MilestoneAttachmentHandler(_blobService.Object, _loggerFactory.Object);

            handler.StoreAttachmentsToBlob(milestoneDto);
            var milestoneAttachmentsCount = milestoneDto.MilestoneAttachments.Count;

            Assert.False(list.Any());
            Assert.Equal(milestoneAttachmentsCount, list.Count);

            _blobService.Verify(x => x.AddToBlobForMilestoneDto(It.Is<Enums.FileFor>(y => y == Enums.FileFor.JobAttachments), It.IsAny<MilestoneAttachmentDto>()), Times.Never);
        }


        private MilestoneDto CreateNewJob()
        {
            var contract = Contract.Get();
            var milestomeDto = new MilestoneDto
            {
                Id = Jobs.Job16.Id,
                CreatedDate = Jobs.Job1.CreatedDate,
                ModifiedDate = Jobs.Job1.ModifiedDate,
                ContractId = contract[0].Id,
                CreatedId = Users.FreelancerFirst.Id,
                MilestoneAttachments = new List<MilestoneAttachmentDto>()
            };
            return milestomeDto;
        }

        //[Fact(Skip = "Errors found in these tests")]
        [Fact]
        public void ConvertFileFromMilestoneReader()
        {
            var reader = new MilestoneAttachmentReader(_blobService.Object);
           
            var milestoneAttachmentDto = MilestoneAttachmentMock.milestoneAttachment1;

            var milestoneAttachment = Mapper.Map<MilestoneAttachment>(milestoneAttachmentDto);

           var result= reader.Convert(milestoneAttachment, milestoneAttachmentDto, null);

            Assert.True(result.Id!=null);
        }
    }
}
