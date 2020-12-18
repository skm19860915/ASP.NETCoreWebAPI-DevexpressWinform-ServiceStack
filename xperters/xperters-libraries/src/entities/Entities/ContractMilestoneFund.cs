using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
   public class ContractMilestoneFund:BaseEntity
    {
        public Guid UserId  { get; set; }
        public Guid MilestoneId { get; set; }
        public int FundStatus { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
        public virtual Milestone Milestone { get; set; }
    }
}
