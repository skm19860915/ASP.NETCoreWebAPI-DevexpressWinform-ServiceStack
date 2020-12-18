using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class Milestone : BaseEntity
    {
        [Column(TypeName = "varchar(1000)")]
        public string MilestoneDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public int MilestoneStatus { get; set; }
        public Guid CreatedId { get; set; }

        public Guid ContractId { get; set; }
        public virtual JobContract Contract  { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual ICollection<ContractMilestoneFund> ContractFunds { get; set; }
        public virtual ICollection<MilestoneMessage> MilestoneMessages { get; set; }
        public virtual ICollection<MilestoneAttachment> MilestoneAttachments { get; set; }
        public virtual ICollection<MilestoneRequestPayer> MilestoneRequestPayers { get; set; }
        public virtual ICollection<MilestoneSystemRequestPayer> MilestoneSystemRequestPayers { get; set; }
    }
}
