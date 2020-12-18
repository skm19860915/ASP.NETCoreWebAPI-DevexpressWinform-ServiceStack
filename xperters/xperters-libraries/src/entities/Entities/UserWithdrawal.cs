using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xperters.enums;

namespace xperters.entities.Entities
{
    public class UserWithdrawal : BaseReadOnlyEntity
    {
        public Guid UserId { get; set; }            

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal BalanceOld { get; set; }            
        
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal BalanceNew { get; set; }
        
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        
        [ForeignKey("PayerStatusId")]
        public int PayerStatusId { get; set; }
        
        [Column(TypeName = "varchar(1024)")]
        public string ResponseMessage { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }            // whether this is a credit or debit transaction

        public PaymentTransactionType PaymentTransactionType { get; set; }
        public virtual RequestPayerStatus PayerStatus { get; set; }
        public virtual User User { get; set; }        
    }
}
