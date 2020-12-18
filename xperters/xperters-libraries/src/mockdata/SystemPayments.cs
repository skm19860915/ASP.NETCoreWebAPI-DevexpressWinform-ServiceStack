using System;
using System.Collections.Generic;
using Bogus;
using xperters.constants;
using xperters.domain;
using xperters.mockdata.Extensions;

namespace xperters.mockdata
{
    public static class SystemPayments
    {
        private static readonly List<SystemPaymentDto> SystemPaymentList;
        public static SystemPaymentDto SystemPayment1 => Get()[0];
        public const int SystemPaymentsCount = 3000;

        static SystemPayments()
        {
            var systemPaymentId = Guid.Parse("{20000000-0000-0000-0000-000000000001}");

            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(Users.RandomSeed);
            var clients = Users.Clients;
            var mrps = MilestoneRequestPayers.Get();
            var created = DateTime.Now.AddYears(-3);

            var systemPaymentFakes = new Faker<SystemPaymentDto>()
                .RuleFor(o => o.Id, f =>
                {

                    var oldguid = systemPaymentId;
                    systemPaymentId = systemPaymentId.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.FromUserId, f => f.PickRandom(clients).Id)
                .RuleFor(o => o.ToUserId, f => SystemConstants.SystemUserId)
                .RuleFor(o => o.MilestoneRequestPayerId, f => f.PickRandom(mrps).Id)
                .RuleFor(o => o.PaymentTransactionTypeId, f => enums.Enums.PaymentTransactionType.Credit)
                .RuleFor(o => o.CurrencyId, 1)
                .RuleFor(o => o.Amount, f => f.Random.Decimal(50, 5001))
                .RuleFor(o => o.FeeFlat, FakeDataConstants.FeeFlatRate)
                .RuleFor(o => o.FeePercent, (f, u) => FakeDataConstants.FeePercent)
                .RuleFor(o => o.TotalAmount, (f, u) => u.Amount + u.FeeFlat + (u.Amount * FakeDataConstants.FeePercent))
                .RuleFor(o => o.Balance, f => f.Random.Decimal(50, 5001))
                .RuleFor(o => o.CreatedDate, f => f.Date.Recent());

            SystemPaymentList = systemPaymentFakes.Generate(SystemPaymentsCount);
        }

        public static List<SystemPaymentDto> Get()
        {
            return SystemPaymentList;
        }
    }
}
