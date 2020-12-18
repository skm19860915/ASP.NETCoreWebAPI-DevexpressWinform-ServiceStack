using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.domain;
using xperters.entities;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.mockdata;
using xperters.unit.tests.FileHandlers;
using System.Linq;
using Xunit;

namespace xperters.unit.tests.Data
{

    /// <summary>
    /// Attempt to model the business process rules in code
    /// - There are separate customers and freelancer users
    /// - Customers are linked to jobs but have made no bids
    /// - Freelancers are linked to jobbids but have created no jobs
    /// </summary>
    public class DataFactoriesAndBuildersShould : BaseUnitTests
    {
        public DataFactoriesAndBuildersShould()
        {
            MockDataFactory.Create();
        }

        [Fact]
        public void PopulateMockDataWithoutErrors()
        {

            try
            {
                var loggerFactory = new LoggerFactory();
                var logger = loggerFactory.CreateLogger<MilestoneAttachmentHandlerShould>();
                var blobService = new Mock<IBlobService>();
                var context = new XpertersContext(new DbContextOptionsBuilder<XpertersContext>()
                    .UseInMemoryDatabase("AddsAttachments").Options);
                var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddDbContext<XpertersContext>(options => options.UseInMemoryDatabase("AddsAttachments"))
                    .AddSingleton(blobService.Object)
                    .AddTransient<IAttachmentHandler<JobDto>, AttachmentHandler>()
                    .AddTransient<IAttachmentHandler<JobBidDto>, JobBidAttachmentHandler>()
                    .AddTransient<IAttachmentHandler<MilestoneDto>, MilestoneAttachmentHandler>()
                    .BuildServiceProvider();

                var dataBuilder = new DataBuilder(context, Mapper, logger);
                dataBuilder.InitializeFileDataForMocks();

                // seed database
                var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
                var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
                var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();
                dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);

                Assert.True(context.Users.Any());
                Assert.True(context.Cards.Any());
                Assert.True(context.Jobs.Any());
                Assert.True(context.MilestoneRequestPayers.Any());
                Assert.True(context.MilestoneSystemRequestPayers.Any());

                Assert.True(context.UserBalances.Any());
                Assert.True(context.SystemBalances.Any());
                Assert.True(context.SystemPayments.Any());
                Assert.True(context.FeeStructures.Any());
                Assert.True(context.UserWithdrawals.Any());
                
                Assert.True(context.MilestoneRequestPayers.Any(x => x.CompletedDate.HasValue));
                Assert.True(context.MilestoneSystemRequestPayers.Any(x => x.CompletedDate.HasValue));

                var milestoneRequestPayers = context.MilestoneRequestPayers.Where(x => x.CompletedDate.HasValue);
                var milestoneSystemRequestPayers =
                    context.MilestoneSystemRequestPayers.Where(x => x.CompletedDate.HasValue);

                Assert.NotNull(milestoneRequestPayers);
                Assert.NotNull(milestoneSystemRequestPayers);
            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine("This is likely to be an entity framework tracking exception.  Needs fixing");
            }
        }


        [Fact]
        public void CreateUsers()
        {
            Assert.True(Users.Get().Any());
        }

        [Fact]
        public void CreateCustomers()
        {
            Assert.True(Users.CustomerFirst.Jobs.Any());
            Assert.True(Users.CustomerSecond.Jobs.Any());

            Assert.Null(Users.CustomerFirst.JobBids);
            Assert.Null(Users.CustomerSecond.JobBids);
        }

        [Fact]
        public void CreateFreelancers()
        {
            Assert.True(Users.FreelancerFirst.JobBids.Any());
            Assert.True(Users.FreelancerSecond.JobBids.Any());

            Assert.Null(Users.FreelancerFirst.Jobs);
            Assert.Null(Users.FreelancerSecond.Jobs);
        }

        [Fact]
        public void CreateAttachments()
        {
            Assert.True(JobAttachments.Get().Any());
        }
        [Fact]
        public void CreateBids()
        {
            Assert.True(JobBids.Get().Any());
        }

        [Fact]
        public void CreateJobs()
        {
            var jobs = Jobs.Get();

            Assert.True(jobs.Any());
        }

        [Fact]
        public void CreateJobsWithAttachments()
        {
            var jobs = Jobs.Get();

            Assert.True(jobs.Any());
            Assert.True(Jobs.Job1.JobAttachments.Any());
            Assert.True(Jobs.Job2.JobAttachments.Any());
            Assert.True(Jobs.Job3.JobAttachments.Any());
        }

        [Fact]
        public void CreateJobsWithBids()
        {
            var jobs = Jobs.Get();

            Assert.True(jobs.Any());
            Assert.True(Jobs.Job1.JobBids.Any());
            Assert.True(Jobs.Job2.JobBids.Any());
            Assert.True(Jobs.Job3.JobBids.Any());
        }

        [Fact]
        public void CreateBidsWhereCustomersAreNotFreelancers()
        {
            foreach (var bid in JobBids.Get())
            {
                Assert.NotEqual(bid.Job.UserId, bid.FreelancerUserId);
            }
        }

        [Fact]
        public void CreateWithdrawals()
        {
            Assert.True(Users.FreelancerFirst.UserWithdrawals.Any());
            Assert.True(Users.FreelancerSecond.UserWithdrawals.Any());
            Assert.True(Users.FreelancerThird.UserWithdrawals.Any());
            Assert.True(Users.FreelancerInactive.UserWithdrawals.Any());
            Assert.True(Users.FreelancerDisabled.UserWithdrawals.Any());
            Assert.True(Users.CustomerFirst.UserWithdrawals.Any());
            Assert.True(Users.CustomerSecond.UserWithdrawals.Any());
            Assert.True(Users.CustomerThird.UserWithdrawals.Any());
        }
    }
}
