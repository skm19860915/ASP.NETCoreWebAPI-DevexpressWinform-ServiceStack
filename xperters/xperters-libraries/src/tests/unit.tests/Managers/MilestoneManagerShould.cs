using Moq;
using Microsoft.AspNetCore.Http;
using xperters.business;
using xperters.entities.Entities;
using xperters.repositories;
using xperters.domain;
using Xunit;
using System.Linq;
using xperters.mockdata;
using System.Collections.Generic;

namespace xperters.unit.tests.Managers
{
    public class MilestoneManagerShould : BaseUnitTests
    {
        private readonly Mock<IRepository<Milestone>> _milestoneRepository;
        private readonly Mock<IRepository<MilestoneMessage>> _milestoneMessageRepository;
        private readonly Mock<IRepository<MilestoneRequestPayer>> _milestoneRequestPayerRepository;
        private readonly Mock<IRepository<MilestoneSystemRequestPayer>> _milestoneSystemRequestPayerRepository;
        private readonly Mock<IRepository<User>> _accountsRepository;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;

        public MilestoneManagerShould()
        {
            _milestoneRepository = new Mock<IRepository<Milestone>>();
            _milestoneMessageRepository = new Mock<IRepository<MilestoneMessage>>();
            _milestoneRequestPayerRepository = new Mock<IRepository<MilestoneRequestPayer>>();
            _milestoneSystemRequestPayerRepository = new Mock<IRepository<MilestoneSystemRequestPayer>>();
            _accountsRepository = new Mock<IRepository<User>>();

            var milestoneDtos = Milestones.Get();
            var milestones = Mapper.Map<List<Milestone>>(milestoneDtos);
            _milestoneRepository.Setup(x => x.Get()).Returns(milestones.AsQueryable);

            var list = Mapper.Map<List<User>>(Users.Get());
            _accountsRepository.Setup(x => x.Get()).Returns(list.AsQueryable);

            var mm = Mapper.Map<List<MilestoneMessage>>(MilestoneMessages.Get());
            _milestoneMessageRepository.Setup(x => x.Get()).Returns(mm.AsQueryable());

            var requestPayerDtos = MilestoneRequestPayers.Get();
            var mrp = Mapper.Map<List<MilestoneRequestPayer>>(requestPayerDtos);
            _milestoneRequestPayerRepository.Setup(x => x.Get()).Returns(mrp.AsQueryable());

            var msrp = Mapper.Map<List<MilestoneSystemRequestPayer>>(MilestoneSystemRequestPayers.Get());
            _milestoneSystemRequestPayerRepository.Setup(x => x.Get()).Returns(msrp.AsQueryable());


            var user = new UserDto
            {
                Email = "joe@bloggs.com",
                FirstName = "Joe",
                LastName = "Bloggs"
            };
            var displayName = $"{user.FirstName} {user.LastName}";

            _httpContextAccessor = CreateHttpContext(displayName, user);
        }

        [Fact]
        public void ShouldReturnMilestonesForApproval10PerPage()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                 , _milestoneMessageRepository.Object
                                                 , _milestoneRequestPayerRepository.Object
                                                 , _milestoneSystemRequestPayerRepository.Object
                                                 , _accountsRepository.Object
                                                 , Mapper
                                                 , LoggerFactory
                                                 , _httpContextAccessor.Object);

            var result = manager.GetMilestonePaymentsForApproval(1, 10);

            Assert.True(result.Any());
        }        
        
        [Fact]
        public void ShouldReturnMilestonesForApproval2PerPage()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                 , _milestoneMessageRepository.Object
                                                 , _milestoneRequestPayerRepository.Object
                                                 , _milestoneSystemRequestPayerRepository.Object
                                                 , _accountsRepository.Object
                                                 , Mapper
                                                 , LoggerFactory
                                                 , _httpContextAccessor.Object);

            var result = manager.GetMilestonePaymentsForApproval(1, 2);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ShouldReturnMilestonesForApproval1PerPage()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                , _milestoneMessageRepository.Object
                                                , _milestoneRequestPayerRepository.Object
                                                , _milestoneSystemRequestPayerRepository.Object
                                                , _accountsRepository.Object
                                                , Mapper
                                                , LoggerFactory
                                                , _httpContextAccessor.Object);

            var result = manager.GetMilestonePaymentsForApproval(1, 1);

            Assert.Single(result);
        }

        [Fact]
        public void ShouldReturnMilestonesForApproval1PerPage2()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                , _milestoneMessageRepository.Object
                                                , _milestoneRequestPayerRepository.Object
                                                , _milestoneSystemRequestPayerRepository.Object
                                                , _accountsRepository.Object
                                                , Mapper
                                                , LoggerFactory
                                                , _httpContextAccessor.Object);

            var result = manager.GetMilestonePaymentsForApproval(2, 1);

            Assert.Single(result);
        }

        [Fact]
        public void ShouldReturnMilestonesForApproval2PerPage2()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                , _milestoneMessageRepository.Object
                                                , _milestoneRequestPayerRepository.Object
                                                , _milestoneSystemRequestPayerRepository.Object
                                                , _accountsRepository.Object
                                                , Mapper
                                                , LoggerFactory
                                                , _httpContextAccessor.Object);

            var result = manager.GetMilestonePaymentsForApproval(2, 2);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ShouldReturnMilestoneIdThatIsApprovedSingle()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                                                , _milestoneMessageRepository.Object
                                                , _milestoneRequestPayerRepository.Object
                                                , _milestoneSystemRequestPayerRepository.Object
                                                , _accountsRepository.Object
                                                , Mapper
                                                , LoggerFactory
                                                , _httpContextAccessor.Object);

            var idB = Milestones.MilestoneB.Id;

            var result = manager.UpdateMilestonePaymentsForAdminApproval(new List<System.Guid>{idB});

            Assert.Single(result);
            Assert.Equal(idB, result[0]);
        }   
        
        [Fact]
        public void ShouldReturnMilestoneIdThatIsApprovedMultiple()
        {

            var manager = new MilestoneManager(_milestoneRepository.Object
                , _milestoneMessageRepository.Object
                , _milestoneRequestPayerRepository.Object
                , _milestoneSystemRequestPayerRepository.Object
                , _accountsRepository.Object
                , Mapper
                , LoggerFactory
                , _httpContextAccessor.Object);

            var idA = Milestones.MilestoneA.Id;
            var idB = Milestones.MilestoneB.Id;

            var result = manager.UpdateMilestonePaymentsForAdminApproval(new List<System.Guid>{idA, idB});

            Assert.Equal(2, result.Count);
            Assert.Equal(idA, result[0]);
            Assert.Equal(idB, result[1]);
        }            
    }
}