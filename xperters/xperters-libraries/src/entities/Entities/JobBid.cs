using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class JobBid : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar(4000)")]
        public string Message { get; set; }
        public Guid JobId { get; set; }
        public Guid FreelancerUserId { get; set; }
        public virtual Job Job { get; set; }
        public virtual User FreelancerUser { get; set; }
        public virtual ICollection<JobBidAttachment> JobBidAttachments { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BidAmount { get; set; }
        public int BidStatus { get; set; }
    }
}

