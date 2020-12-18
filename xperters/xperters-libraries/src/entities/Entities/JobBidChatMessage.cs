using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class JobBidChatMessage : BaseEntity
    {
        
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid JobBidChatSessionId { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Message { get; set; }
        public int MessageType { get; set; }
        public virtual User Sender { get; set; }    
        public virtual JobBidChatSession JobBidChatSession { get; set; }

    }
}
