using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class BaseRequestPayer : BaseEntity
    {
        public Guid MilestoneId { get; set; }

        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }

        [Column(TypeName = "decimal")]
        [Required]
        public decimal Amount { get; set; }
        [Column(TypeName = "varchar(1024)")]
        public string ResponseMessage { get; set; }
        public DateTime? CompletedDate { get; set; }

        [ForeignKey("PayerStatusId")]
        public int PayerStatusId { get; set; }
        public bool IsActive { get; set; }
        public int? PaymentServiceCheckCount { get; set; }

        public DateTime? LastPaymentServiceStatusCheck { get; set; }

        public virtual RequestPayerStatus PayerStatus { get; set; }

        public virtual Milestone Milestone { get; set; }
    }
}
