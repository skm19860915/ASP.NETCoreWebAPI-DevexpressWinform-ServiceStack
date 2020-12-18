using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;
using xperters.fileutilities.Files;
using xperters.fileutilities.Interfaces;
using xperters.mockdata;
using xperters.unit.tests.FileHandlers;
using Xunit;

namespace xperters.unit.tests.Entities
{
    public class PaymentsShould : BaseUnitTests
    {
        private XpertersContext _context;

        [Fact]
        public void MakeSystemPaymentMapping()
        {
            var dto = SystemPayments.SystemPayment1;
            var systemPayment = Mapper.Map<SystemPayment>(dto);

            Assert.Equal(dto.Id, systemPayment.Id);
            Assert.Equal(dto.CreatedDate, systemPayment.CreatedDate);
            Assert.Equal(dto.FromUserId, systemPayment.FromUserId);
            Assert.Equal(dto.ToUserId, systemPayment.ToUserId);
            Assert.Equal(dto.Balance, systemPayment.Balance);
            Assert.Equal(dto.PaymentTransactionTypeId, systemPayment.PaymentTransactionTypeId);
            Assert.Equal(dto.FromUserId, systemPayment.FromUserId);
        }        
        
        [Fact]
        public void MakeUserPaymentMapping()
        {
            var dto = UserPaymentsMock.UserPayment1;
            var payment = Mapper.Map<UserPayment>(dto);

            Assert.Equal(dto.Id, payment.Id);
            Assert.Equal(dto.CreatedDate, payment.CreatedDate);
            Assert.Equal(dto.FromUserId, payment.FromUserId);
            Assert.Equal(dto.ToUserId, payment.ToUserId);
            Assert.Equal(dto.Balance, payment.Balance);
            Assert.Equal(dto.PaymentTransactionTypeId, payment.PaymentTransactionTypeId);
            Assert.Equal(dto.FromUserId, payment.FromUserId);
        }

        [Fact]
        public void MakeUserBalanceMapping()
        {
            var dto = UserBalancesMock.UserBalance1;
            var balance = Mapper.Map<UserBalance>(dto);

            Assert.Equal(dto.Id, balance.Id);
            Assert.Equal(dto.UserId, balance.UserId);
            Assert.Equal(dto.CreatedDate, balance.CreatedDate);
            Assert.Equal(dto.ModifiedDate, balance.ModifiedDate);
            Assert.Equal(dto.Balance, balance.Balance);
            Assert.Equal(dto.BalancePrevious, balance.BalancePrevious);
        }

        [Fact]
        public void MakeSystemBalanceMapping()
        {
            var dto = SystemBalancesMock.SystemBalance1;
            var balance = Mapper.Map<SystemBalance>(dto);

            Assert.Equal(dto.Id, balance.Id);
            Assert.Equal(dto.CreatedDate, balance.CreatedDate);
            Assert.Equal(dto.ModifiedDate, balance.ModifiedDate);
            Assert.Equal(dto.Balance, balance.Balance);
            Assert.Equal(dto.BalancePrevious, balance.BalancePrevious);
        }

        [Fact(Skip = "problem with context again")]
        public void GetUserPayments()
        {
            InitializeContext();
            var userId1 = new Guid(Users.ClientId1);
            var user1 = _context.Users.First(u => u.Id == userId1);
            var payments1 = _context.UserPayments.Where(p => p.FromUserId == user1.Id);

            Assert.True(_context.Users.Any());
            Assert.True(payments1.Any());

            var userId2 = new Guid(Users.ClientId2);
            var user2 = _context.Users.First(u => u.Id == userId2);
            var payments2 = _context.UserPayments.Where(p => p.FromUserId == user2.Id);

            Assert.True(payments2.Any());

            var userId3 = new Guid(Users.ClientId3);
            var user3 = _context.Users.First(u => u.Id == userId3);
            var payments3 = _context.UserPayments.Where(p => p.FromUserId == user3.Id);

            Assert.True(payments3.Any());
        }


        private void InitializeContext(){
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<MilestoneAttachmentHandlerShould>();
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

            var dataBuilder = new DataBuilder(_context, Mapper, logger);
            dataBuilder.InitializeFileDataForMocks();

            // seed database
            var attachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobDto>>();
            var bidAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<JobBidDto>>();
            var milestoneAttachmentHandler = serviceProvider.GetService<IAttachmentHandler<MilestoneDto>>();
            dataBuilder.ApplyMockData(attachmentHandler, bidAttachmentHandler, milestoneAttachmentHandler);
        }
    }
}
