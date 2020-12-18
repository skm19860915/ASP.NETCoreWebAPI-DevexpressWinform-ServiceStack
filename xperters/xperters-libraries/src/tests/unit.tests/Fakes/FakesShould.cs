using System;
using System.Linq;
using xperters.enums;
using xperters.extensions;
using xperters.mockdata;
using xperters.mockdata.Extensions;
using Xunit;

namespace xperters.unit.tests.Fakes
{
    public class FakesShould
    {

        [Fact]
        public void CreateUsers()
        {
            var users = Users.Get();
            var usersCount = users.Count();
            var customerFirst = Users.CustomerFirst;
            
            Assert.Equal(Users.UsersCount, usersCount);
            Assert.True(customerFirst.Id != Guid.Empty);
            Assert.True(customerFirst.FirstName.IsNotBlank());
            Assert.True(customerFirst.LastName.IsNotBlank());
            Assert.True(customerFirst.DisplayName.IsNotBlank());
            Assert.True(customerFirst.Avatar.IsNotBlank());
        }        
        
        [Fact]
        public void CreateJobs()
        {
            var jobsCount = Jobs.Get().Count();
            var job1 = Jobs.Job1;
            
            Assert.Equal(Jobs.JobsCount, jobsCount);
            Assert.True(job1.Id != Guid.Empty);
            Assert.True(job1.JobTitle.IsNotBlank());
            Assert.True(job1.Description.IsNotBlank());
            Assert.True(job1.JobPrice > 0);
            Assert.True(job1.JobStatusId > 0);            
        }
        
        [Fact]
        public void CreateJobContracts()
        {
            var contracts = JobContracts.Get();
            
            // ensure the milestones are also initialized;
            var milestones = Milestones.Get();
            
            var contractsCount = contracts.Count;
            var contract = contracts.First(x=>x.Milestones.Any());

            // contracts with a large number of milestones
            // ensures there's a good spread f milestones across the contracts
            var contractLarge = contracts.FirstOrDefault(x=>x.Milestones.Count > 100);
            Assert.Null(contractLarge);

            Assert.Equal(JobContracts.JobContractsCount, contractsCount);
            
            Assert.NotEqual(Guid.Empty, contract.Id);
            Assert.NotEqual(Guid.Empty, contract.JobId);
            Assert.NotEqual(Guid.Empty,contract.FreelancerId);
            Assert.NotEqual(DateTime.MinValue, contract.CreatedDate);
            Assert.NotEqual(DateTime.MinValue, contract.ModifiedDate);
            Assert.NotEqual(DateTime.MinValue, contract.ContractStartDate);
            Assert.NotEqual(0m, contract.Amount);
            Assert.NotEqual(0, contract.ContractStatus);
            Assert.True(contract.Milestones.Any());
            Assert.NotNull(contract.Job);
            Assert.NotNull(contract.Freelancer);
        }        

        [Fact]
        public void CreateMilestones()
        {
            var milestones = Milestones.Get();
            
            var milestonesCount = milestones.Count;
            var milestone = milestones.First();
            var milestonesEmptyId = milestones.Any(x=>x.Id == Guid.Empty);
            
            Assert.False(milestonesEmptyId);
            Assert.Equal(Milestones.MilestoneCount, milestonesCount);
            
            Assert.NotEqual(Guid.Empty, milestone.Id);
            Assert.NotEqual(Guid.Empty, milestone.ContractId);
            Assert.NotNull(milestone.Contract);
            Assert.NotEqual(0,milestone.MilestoneStatus);
            Assert.True(milestone.Description.IsNotBlank());
            Assert.NotEqual(0m, milestone.Amount);
            Assert.NotEqual(DateTime.MinValue, milestone.CreatedDate);
            Assert.NotEqual(DateTime.MinValue, milestone.ModifiedDate);
            
            Assert.True(milestone.DueDate > milestone.ModifiedDate);
        } 
        
        [Fact]
        public void CreateMilestonesInClientApprovedStatus()
        {
            var milestonesInClientApprovedStatus = Milestones.Get()
                                                        .Where(x=>x.MilestoneStatus == (int)Enums.MilestoneStatus.ClientApproved)
                                                        .ToList();
            
            var count = milestonesInClientApprovedStatus.Count();
            var foundMilestonesInClientApprovedStatus = milestonesInClientApprovedStatus.Any();
            
            Assert.True(count > 0 && count != Milestones.MilestoneCount);
            Assert.True(foundMilestonesInClientApprovedStatus);
        }   
        
        [Fact]
        public void CreateMilestonesInClientApprovedStatusWithRequestPayers()
        {
            var milestones = Milestones.Get();
            var mrps = MilestoneRequestPayers.Get();
            
            var milestonesInClientApprovedStatus = milestones
                .Where(x=>x.MilestoneStatus == (int)Enums.MilestoneStatus.ClientApproved )
                .Where(x=>x.MilestoneRequestPayers!=null)
                .ToList();
            
            var count = milestonesInClientApprovedStatus.Count;
            var foundMilestonesInClientApprovedStatus = milestonesInClientApprovedStatus.Any();
            
            Assert.True(count > 0 && count != Milestones.MilestoneCount);
            Assert.True(foundMilestonesInClientApprovedStatus);
        }        
        
        [Fact]
        public void CreateMilestoneRequestPayers()
        {
            var mrp = MilestoneRequestPayers.Get();
            var mrpCount = mrp.Count;
            var milestoneRequestPayer = mrp.First();
            var totalAmount = milestoneRequestPayer.Amount + milestoneRequestPayer.FeeFlat + (milestoneRequestPayer.Amount * 0.05m);
            
            Assert.Equal(MilestoneRequestPayers.MilestoneRequestPayersCount, mrpCount);
            
            Assert.NotEqual(Guid.Empty, milestoneRequestPayer.Id);
            Assert.NotEqual(Guid.Empty, milestoneRequestPayer.MilestoneId);
            Assert.NotEqual(0, milestoneRequestPayer.CurrencyId);
            Assert.NotEqual(0m, milestoneRequestPayer.Amount);
            Assert.NotEqual(0, milestoneRequestPayer.PayerStatusId);
            Assert.NotNull(milestoneRequestPayer.PayerStatus);
            Assert.True(milestoneRequestPayer.ResponseMessage.IsNotBlank());

            Assert.Equal(FakeDataConstants.FeeFlatRate, milestoneRequestPayer.FeeFlat);
            Assert.Equal(FakeDataConstants.FeePercent, milestoneRequestPayer.FeePercent);
            Assert.Equal(FakeDataConstants.FeeFlatRate + (FakeDataConstants.FeePercent * milestoneRequestPayer.Amount), milestoneRequestPayer.FeeTotal);
            Assert.Equal(totalAmount, milestoneRequestPayer.TotalAmount);

            Assert.NotEqual(0, milestoneRequestPayer.PaymentServiceCheckCount);
            Assert.NotEqual(DateTime.MinValue, milestoneRequestPayer.LastPaymentServiceStatusCheck);
            Assert.NotEqual(DateTime.MinValue, milestoneRequestPayer.CreatedDate);
            Assert.NotEqual(DateTime.MinValue, milestoneRequestPayer.ModifiedDate);
        }
        
        [Fact]
        public void CreateSystemPayments()
        {
            var systemPayments = SystemPayments.Get();
            var systemPaymentsCount = systemPayments.Count;
            var systemPayment = systemPayments.First();

            var totalAmount = systemPayment.Amount + systemPayment.FeeFlat + (systemPayment.Amount * 0.05m);
            Assert.Equal(SystemPayments.SystemPaymentsCount, systemPaymentsCount);
            
            Assert.NotEqual(Guid.Empty, systemPayment.Id);
            Assert.NotEqual(Guid.Empty, systemPayment.FromUserId);
            Assert.NotEqual(Guid.Empty, systemPayment.ToUserId);
            Assert.NotEqual(Guid.Empty, systemPayment.MilestoneRequestPayerId);
            Assert.True(systemPayment.CurrencyId > 0);
            Assert.True(systemPayment.Amount > 49m);
            Assert.Equal(FakeDataConstants.FeeFlatRate, systemPayment.FeeFlat);
            Assert.Equal(FakeDataConstants.FeePercent, systemPayment.FeePercent);
            Assert.Equal(totalAmount, systemPayment.TotalAmount);
            Assert.True(systemPayment.Balance > 0m);
            Assert.NotEqual(DateTime.MinValue, systemPayment.CreatedDate);
        }          

        [Fact]
        public void IncrementGuid()
        {
            var id = Guid.Parse("{10000000-0000-0000-0000-000000000001}");
            var expectedId = Guid.Parse("{10000000-0000-0000-0000-000000000002}");
            var result = id.Increment();
            
            Assert.Equal(expectedId, result);
        }
    }
}