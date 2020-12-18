using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public static class UserBalancesMock
    {
        private static readonly List<UserBalanceDto> UserBalances;
        public static UserBalanceDto UserBalance1 => Get()[0];
        public static UserBalanceDto UserBalance2 => Get()[1];
        public static UserBalanceDto UserBalance3 => Get()[2];


        static UserBalancesMock()
        {
            UserBalances = new List<UserBalanceDto>
            {
                new UserBalanceDto
                {
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    UserId = new Guid(Users.ClientId1),
                    UserPaymentId = new Guid(FakeDataConstants.UserPaymentId1),
                    Balance = 2m,
                    BalancePrevious = 1m,
                    TransactionAmount = 1m
                },
                new UserBalanceDto
                {
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    UserId = new Guid(Users.ClientId2),
                    UserPaymentId = new Guid(FakeDataConstants.UserPaymentId2),
                    Balance = 3m,
                    BalancePrevious = 2m,
                    TransactionAmount = 1m
                },
                new UserBalanceDto
                {
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,                    
                    UserId = new Guid(Users.ClientId3),
                    UserPaymentId = new Guid(FakeDataConstants.UserPaymentId3),
                    Balance = 5m,
                    BalancePrevious = 3m,
                    TransactionAmount = 2m
                }
            };
        }

        public static List<UserBalanceDto> Get()
        {
            return UserBalances;
        }
    }
}
