using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using xperters.enums;

namespace xperters.entities.Entities
{
    public class UserPayment : BaseReadOnlyEntity
    {
        public Guid FromUserId { get; set; }            // This needs to be an actual user Id.  Never System.  System should only appear in the systempayments table
        public Guid ToUserId { get; set; }              // This needs to be an actual user Id.  Never System
        public Guid MilestoneRequestPayerId { get; set; }
        public Guid MilestoneSystemRequestPayerId { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }            // whether this is a credit or debit transaction
        public PaymentTransactionType PaymentTransactionType { get; set; }
        public int CurrencyId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }            // User balance

        public User FromUser { get; set; }
        public User ToUser { get; set; }

        public ICollection<UserBalance> UserBalances { get; set; }

    }
}
