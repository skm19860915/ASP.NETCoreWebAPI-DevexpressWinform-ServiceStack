using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class MilestoneRequestPayer : BaseRequestPayer
    {
        public Guid ClientId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeeFlat { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeePercent { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FeeTotal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
    }
}
