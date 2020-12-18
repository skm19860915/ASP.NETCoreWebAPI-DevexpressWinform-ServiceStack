using System;
using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
   public class ContractChatSessionUser:BaseEntity
    {
        [Required]
        public Guid ContractChatSessionId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual ContractChatSession ContractChatSession { get; set; }
        public virtual User User { get; set; }
    }
}
