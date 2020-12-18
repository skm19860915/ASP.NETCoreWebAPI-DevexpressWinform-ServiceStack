using System;

namespace xperters.domain
{
    /// <summary>
    /// All user balances
    /// </summary>
    public class UserBalanceDto: BaseDto
    {
        public Guid UserId { get; set; }
        public Guid UserPaymentId { get; set; }

        public decimal Balance { get; set; }         
        public decimal BalancePrevious { get; set; }
        public decimal TransactionAmount { get; set; }

    }
}