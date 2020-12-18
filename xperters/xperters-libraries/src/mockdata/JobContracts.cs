using System;
using System.Collections.Generic;
using Bogus;
using xperters.domain;
using xperters.enums;
using xperters.mockdata.Extensions;

namespace xperters.mockdata
{
    public class JobContracts
    {
        public static Guid JobContractId1;

        private static readonly List<JobContractDto> JobContractsList;
        public const int JobContractsCount = 300;

        static JobContracts()
        {
            JobContractId1 = Guid.Parse("{41000000-0000-0000-0000-000000000000}");
            
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(Users.RandomSeed);
            var jobs = Jobs.Get();
            var freelancers = Users.Freelancers;

            var jobContractFakes = new Faker<JobContractDto>()
                .RuleFor(o => o.Id, f =>
                {
                    var oldguid = JobContractId1;
                    JobContractId1 = JobContractId1.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.ContractStatus, f => (int)f.PickRandom<Enums.ContractStatus>())
                .RuleFor(o => o.Job, f => f.PickRandom(jobs))
                .RuleFor(o => o.JobId, (f, u) => u.Job.Id)
                .RuleFor(o => o.Freelancer, f  => f.PickRandom(freelancers))
                .RuleFor(o => o.FreelancerId, (f, u) => u.Freelancer.Id)
                .RuleFor(o => o.Milestones, f => new List<MilestoneDto>())
                .RuleFor(o => o.Amount, f=>f.Random.Decimal(50,10001))
                .RuleFor(o => o.CreatedDate, f => f.Date.Recent())
                .RuleFor(o => o.ModifiedDate, (f, u) => u.CreatedDate)
                .RuleFor(o => o.ContractStartDate, (f, u) => u.CreatedDate.AddDays(4));

            JobContractsList = jobContractFakes.Generate(JobContractsCount);                  
        }

        public static JobContractDto JobContract1 => Get()[0];
        public static JobContractDto JobContract2 => Get()[1];
        public static JobContractDto JobContract3 => Get()[2];
        public static JobContractDto JobContract4 => Get()[3];
        public static JobContractDto JobContract5 => Get()[4];

        public static List<JobContractDto> Get()
        {
            return JobContractsList;
        }
    }
}