using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class MilestoneMessage : BaseEntity
    {
        public Guid MilestoneId { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }
        public Guid CreatedId { get; set; }
        public virtual Milestone Milestone { get; set; }
        public virtual User Created { get; set; }
    }
}
