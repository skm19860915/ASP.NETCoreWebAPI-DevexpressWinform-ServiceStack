using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Humanizer;
using xperters.domain;
using xperters.enums;
using xperters.mockdata.Extensions;

namespace xperters.mockdata
{
    public class Milestones
    {
        public static Guid MilestoneId1;
        public static Guid MilestoneId2;
        public static Guid MilestoneId3;
        public static Guid MilestoneId4;
        public static Guid MilestoneId5;
        public static Guid MilestoneId6;
        public static Guid MilestoneId7;
        public static Guid MilestoneId8;
        public static Guid MilestoneId9;
        public static Guid MilestoneId10;

        public const int MilestoneCount = 3000;
        
        private static readonly List<MilestoneDto> MilestoneDtos;
        public static MilestoneDto Milestone1 => Get()[0];
        public static MilestoneDto Milestone2 => Get()[1];
        public static MilestoneDto Milestone3 => Get()[2];
        public static MilestoneDto Milestone4 => Get()[3];
        public static MilestoneDto Milestone5 => Get()[4];

        public static MilestoneDto MilestoneA => Get()[16];
        public static MilestoneDto MilestoneB => Get()[17];

        static Milestones()
        {
            var milestoneId1 = Guid.Parse("{80000000-0000-0000-0000-000000000000}");
            
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(Users.RandomSeed);
            var clients = Users.Clients;
            var contracts = JobContracts.Get();

            var milestones = new Faker<MilestoneDto>()
                .RuleFor(o => o.Id, f =>
                {
                    var oldguid = milestoneId1;
                    milestoneId1 = milestoneId1.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.ContractId, f => f.PickRandom(contracts).Id)
                .RuleFor(o => o.CreatedId, f => f.PickRandom(clients).Id)
                .RuleFor(o => o.MilestoneStatus, f => (int)f.PickRandom<Enums.MilestoneStatus>())
                .RuleFor(o => o.MilestoneRequestPayers, f => new List<MilestoneRequestPayerDto>())
                .RuleFor(o => o.MilestoneSystemRequestPayers, f => new List<MilestoneSystemRequestPayerDto>())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence().Truncate(1000).TrimEnd())
                .RuleFor(o => o.Amount, f=>f.Random.Decimal(50,10001))
                .RuleFor(o => o.CreatedDate, f => f.Date.Recent())
                .RuleFor(o => o.ModifiedDate, (f, u) => u.CreatedDate)
                .RuleFor(o => o.DueDate, (f, u) => u.CreatedDate.AddDays(1));

            MilestoneDtos = milestones.Generate(MilestoneCount);
            
            // Setup the contract relationship
            foreach (var contract in contracts)
            {
                var milestoneList = MilestoneDtos.Where(c=>c.ContractId==contract.Id).ToList();
                
                if (milestoneList.Any())
                {
                    contract.Milestones.AddRange(milestoneList);

                    foreach (var milestone in milestoneList)
                    {
                        milestone.Contract = contract;
                    }
                }
            }            
        }

        public static List<MilestoneDto> Get()
        {
            return MilestoneDtos;
        }
    }
}
