using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
   public class JobContract:BaseEntity
    {
        [Required]
        public Guid JobId { get; set; }
        [Required]
        public Guid FreelancerId { get; set; }
        public DateTime ContractStartDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public int ContractStatus { get; set; }
        public virtual Job Job { get; set; }
        public virtual User Freelancer { get; set; }

        public virtual ICollection<Milestone> Milestones { get; set; }
    }
}
