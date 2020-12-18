using System;
using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class JobBidChatSessionUser : BaseEntity
    {
        [Required]
        public Guid JobBidChatSessionId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual JobBidChatSession JobBidChatSession { get; set; }
        public virtual User User { get; set; }

    }
}
