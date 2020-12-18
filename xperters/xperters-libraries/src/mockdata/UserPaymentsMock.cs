using System;
using System.Collections.Generic;
using xperters.constants;
using xperters.domain;

namespace xperters.mockdata
{
    public static class UserPaymentsMock
    {
        private static readonly List<UserPaymentDto> UserPayments;
        public static UserPaymentDto UserPayment1 => Get()[0];

        static UserPaymentsMock()
        {
            UserPayments = new List<UserPaymentDto>
            {
                new UserPaymentDto
                {
                    Id = new Guid(FakeDataConstants.UserPaymentId1),
                    FromUserId = new Guid(Users.ClientId1),
                    ToUserId = new Guid(Users.ClientId2),
                    MilestoneRequestPayerId = new Guid(FakeDataConstants.MrpId1),
                    PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Credit,
                    CurrencyId = 1,
                    Amount = 23.45M,
                    Balance = 23.45M
                },
                new UserPaymentDto
                {
                    Id = new Guid(FakeDataConstants.UserPaymentId2),
                    FromUserId = new Guid(Users.ClientId2),
                    ToUserId = new Guid(Users.ClientId3),
                    MilestoneRequestPayerId = new Guid(FakeDataConstants.MrpId2),
                    PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Credit,
                    CurrencyId = 1,
                    Amount = 123.45M,
                    Balance = 123.45M
                },
                new UserPaymentDto
                {
                    Id = new Guid(FakeDataConstants.UserPaymentId3),

                    FromUserId = new Guid(Users.ClientId3),
                    ToUserId = new Guid(Users.ClientId2),
                    MilestoneRequestPayerId = new Guid(FakeDataConstants.MrpId3),
                    PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Credit,
                    CurrencyId = 1,
                    Amount = 223.45M,
                    Balance = 223.45M
                },
                new UserPaymentDto
                {
                    Id = new Guid(FakeDataConstants.UserPaymentId4),
                    FromUserId = new Guid(Users.ClientId2),
                    ToUserId = new Guid(Users.ClientId1),
                    MilestoneRequestPayerId = new Guid(FakeDataConstants.MrpId1),
                    PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Debit,
                    CurrencyId = 1,
                    Amount = 323.45M,
                    Balance = 3323.45M
                }
            };
        }

        public static List<UserPaymentDto> Get()
        {
            return UserPayments;
        }
    }
}
