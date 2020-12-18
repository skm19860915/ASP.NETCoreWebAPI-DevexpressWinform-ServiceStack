using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    /// <summary>
    /// All user balances
    /// </summary>
    public class UserBalance : BaseEntity
    {
        public Guid UserPaymentId { get; set; }
        public UserPayment UserPayment { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; } 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalancePrevious { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransactionAmount { get; set; }

    }
}