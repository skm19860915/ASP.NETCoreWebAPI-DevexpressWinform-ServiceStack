using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using xperters.constants;
using xperters.domain;
using xperters.domain.Extensions;
using xperters.mockdata.Extensions;

namespace xperters.mockdata
{
    public class MilestoneRequestPayers
    {
        public const int MilestoneRequestPayersCount = 300;
        // ReSharper disable once InconsistentNaming
        private static readonly List<MilestoneRequestPayerDto> MRPayers;

       
        static MilestoneRequestPayers()
        {
            var mrpId = Guid.Parse(FakeDataConstants.MrpId1);
            
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(Users.RandomSeed);
            var milestones = Milestones.Get();
            var created = DateTime.Now.AddYears(-3);

            var mrpFakes = new Faker<MilestoneRequestPayerDto>()
                .RuleFor(o => o.Id, f =>
                {
                    var oldguid = mrpId;
                    mrpId = mrpId.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.MilestoneId, f => f.PickRandom(milestones).Id)
                .RuleFor(o => o.CurrencyId, 1)
                .RuleFor(o => o.IsActive, true)
                .RuleFor(o => o.Amount, f => f.Random.Decimal(50, 5001))
                .RuleFor(o => o.PayerStatusId, 1)
                .RuleFor(o => o.PayerStatus, new RequestPayerStatusDto
                {
                    PayerStatus = PaymentConstants.Successful,
                    PayerStatusId = 1,
                    IsActive = true
                })
                .RuleFor(o => o.ResponseMessage, "Message")
                .RuleFor(o => o.Amount, f => f.Random.Decimal(50, 5001))
                .RuleFor(o => o.FeeFlat, FakeDataConstants.FeeFlatRate)
                .RuleFor(o => o.FeePercent, (f, u) => FakeDataConstants.FeePercent)
                .RuleFor(o => o.FeeTotal, (f, u) => u.Amount.CalculateTotalFees(FakeDataConstants.FeeFlatRate, FakeDataConstants.FeePercent))
                .RuleFor(o => o.TotalAmount, (f, u) => u.Amount.CalculateTotalAmount(FakeDataConstants.FeeFlatRate, FakeDataConstants.FeePercent))

                .RuleFor(o => o.PaymentServiceCheckCount, f => f.Random.Int(0, 20))
                .RuleFor(o => o.CreatedDate, f => f.Date.Recent())
                .RuleFor(o => o.ModifiedDate,
                    (f, u) => u.CreatedDate.AddHours(f.Random.Int(0, 100)).AddMilliseconds(f.Random.Int(0, 100000)))
                .RuleFor(o => o.LastPaymentServiceStatusCheck,
                    (f, u) => u.ModifiedDate.AddHours(f.Random.Int(0, 100)).AddMilliseconds(f.Random.Int(0, 100000)))
                .RuleFor(o => o.CompletedDate,
                    (f, u) => u.CreatedDate.AddHours(f.Random.Int(0, 100)).AddMilliseconds(f.Random.Int(0, 100000)));          
                
            MRPayers = mrpFakes.Generate(MilestoneRequestPayersCount);
            
            foreach (var mrp in MRPayers)
            {
                var milestoneList = milestones.Where(m=>m.Id==mrp.MilestoneId).ToList();
                
                if (milestoneList.Any())
                {
                    foreach (var milestone in milestoneList)
                    {
                        milestone.MilestoneRequestPayers.Add(mrp);
                    }
                }
            }                       
        }

        public static List<MilestoneRequestPayerDto> Get()
        {
            return MRPayers;
        }
    }
}
