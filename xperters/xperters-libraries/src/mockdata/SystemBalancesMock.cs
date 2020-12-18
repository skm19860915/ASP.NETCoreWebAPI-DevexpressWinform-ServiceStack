using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public static class SystemBalancesMock
    {
        private static readonly List<SystemBalanceDto> SystemBalances;
        public static SystemBalanceDto SystemBalance1 => Get()[0];
        public const string SystemPaymentId1 = "{00000011-0000-0000-0000-000000000001}";

        static SystemBalancesMock()
        {
            SystemBalances = new List<SystemBalanceDto>
            {
                new SystemBalanceDto
                {
                    SystemPaymentId = new Guid(SystemPaymentId1),
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Balance = 123.45M,
                    BalancePrevious = 23.45M,
                    TransactionAmount = 100m
                }
            };
        }

        public static List<SystemBalanceDto> Get()
        {
            return SystemBalances;
        }
    }
}
