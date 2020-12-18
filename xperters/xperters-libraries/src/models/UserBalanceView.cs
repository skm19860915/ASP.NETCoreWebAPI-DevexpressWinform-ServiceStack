using System;

namespace xperters.models
{
    /// <summary>
    /// All user balances
    /// </summary>
    public class UserBalanceView
    {
        public Guid UserId { get; set; }

        public decimal Balance { get; set; }            // system balance

    }
}