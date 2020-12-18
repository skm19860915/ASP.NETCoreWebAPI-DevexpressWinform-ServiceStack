using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.business;
using xperters.configurations;
using xperters.configurations.Interfaces;
using xperters.constants;
using xperters.domain;
using xperters.entities;
using xperters.fileio;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.infrastructure.Extensions;
using xperters.mockdata;
using xperters.models.DataViews;
using xperters.models.DataViews.AdminJob;
using xperters.repositories;
using xperters.tests.common;
using Xunit;

namespace xperters.integration.tests.Managers
{
    public class JobAdminManagerShould : BaseTests
    {

        private readonly IRepositoryReadOnly<JobInformationView> _jobInfoRepository;
        private XpertersContext _context;
        private ILoggerFactory _loggerFactory;
        private IMapper _mapper;

        public JobAdminManagerShould()
        {
            var factory = new LoggerFactory();
            var path = Directory.GetCurrentDirectory();
            var env = new Mock<IHostEnvironment>();
            var services = new ServiceCollection();
            env.Setup(x => x.ContentRootPath).Returns(path);

            var filesHandler = new FilesHandler(env.Object);
            services.AddSingleton<IHandleFiles>(filesHandler);
            services.AddSingleton<ILoggerFactory>(factory);
            services.AddSingleton<ILoggerFactory>(factory);

            var appSettings = new Dictionary<string, string>
            {
                {XpertersEnvVariables.DotnetRunningInContainer, "false"},
            };

            var handler = new EnvironmentHandlerMock(appSettings);
            services.AddSingleton<IHandleEnvironment>(handler);

            var appConfigBuilder = new AppConfigBuilder(env.Object, services);

            var config = appConfigBuilder.Build();
            services.ConfigureDependencies(config);

            services.AddTransient<IRepositoryReadOnly<JobInformationView>, JobInformationRepository>();

            var provider = services.BuildServiceProvider();

            _loggerFactory = provider.GetService<ILoggerFactory>();
            _mapper = provider.GetService<IMapper>();

            InitializeContext();
            services.AddSingleton(_context);
            provider = services.BuildServiceProvider();
            _jobInfoRepository = provider.GetService<IRepositoryReadOnly<JobInformationView>>();
        }

        [Fact]
        public void ShouldReturnJobInformation10PerPage()
        {
            var numberPerPage = 5;
            var manager = new JobAdminManager(_mapper, _loggerFactory, _jobInfoRepository);

            var result = manager.GetJobInformation(1, numberPerPage);
            var infoCreated1 = result.ElementAt(0).Created;
            var infoCreated3 = result.ElementAt(2).Created;
            var infoCreated5 = result.ElementAt(4).Created;

            // Check the number of paged items
            Assert.Equal(numberPerPage, result.Count());

            // Check the sort order
            Assert.True(infoCreated1 > infoCreated3);
            Assert.True(infoCreated3 >= infoCreated5);
        }

        private void InitializeContext()
        {
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<JobAdminManagerShould>();
            var blobService = new Mock<IBlobService>();
            _context = new XpertersContext(new DbContextOptionsBuilder<XpertersContext>().UseInMemoryDatabase("AddsAttachments").Options);

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<XpertersContext>(options => options.UseInMemoryDatabase("AddsAttachments"))
                .AddSingleton(blobService.Object)
                .AddTransient<IAttachmentHandler<JobDto>, AttachmentHandler>()
                .AddTransient<IAttachmentHandler<JobBidDto>, JobBidAttachmentHandler>()
                .AddTransient<IAttachmentHandler<MilestoneDto>, MilestoneAttachmentHandler>()
                .BuildServiceProvider();

            var dataBuilder = new DataBuilder(_context, _mapper, logger);
            dataBuilder.InitializeFileDataForMocks();

            // seed database
            var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
            var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
            var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();
            dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
        }
    }
}
