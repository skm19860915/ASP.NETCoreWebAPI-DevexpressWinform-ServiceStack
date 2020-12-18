using System.Collections.Generic;

namespace xperters.payments.Responses
{
    public class AccountBalancesResponse
    {
        public string Currency { get; set; }
        public IEnumerable<Balance> Balances { get; set; }

        public class Balance
        {
            public decimal Amount { get; set; }
            public string Type { get; set; }
        }
    }
}
