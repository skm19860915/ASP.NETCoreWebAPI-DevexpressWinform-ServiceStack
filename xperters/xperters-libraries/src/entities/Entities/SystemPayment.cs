using System;
using System.ComponentModel.DataAnnotations.Schema;
using xperters.enums;

namespace xperters.entities.Entities
{
    public class SystemPayment : BaseReadOnlyEntity
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public Guid MilestoneRequestPayerId { get; set; }
        public Guid MilestoneSystemRequestPayerId { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }            // whether this is a credit or debit transaction
        public PaymentTransactionType PaymentTransactionType { get; set; }

        public int CurrencyId { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeeFlat { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeePercent { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }            // system balance
    }
}