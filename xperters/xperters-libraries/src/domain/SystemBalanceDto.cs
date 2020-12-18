using System;

namespace xperters.domain
{
    /// <summary>
    /// All user balances
    /// </summary>
    public class SystemBalanceDto : BaseDto
    {
        public Guid SystemPaymentId { get; set; }

        public decimal Balance { get; set; }            
        public decimal BalancePrevious { get; set; } 
        public decimal TransactionAmount { get; set; } 
    }
}