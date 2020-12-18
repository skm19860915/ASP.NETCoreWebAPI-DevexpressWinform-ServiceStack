using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    /// <summary>
    /// Table used to record the current system balance
    /// it is expected that this table will only ever have one record which is only ever updated
    /// </summary>
    public class SystemBalance : BaseEntity
    {
        public Guid SystemPaymentId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; } 
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalancePrevious { get; set; }        
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransactionAmount { get; set; }

    }
}