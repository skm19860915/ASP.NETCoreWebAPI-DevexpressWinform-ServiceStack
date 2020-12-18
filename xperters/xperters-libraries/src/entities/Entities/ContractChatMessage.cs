using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
   public class ContractChatMessage:BaseEntity
    {
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid ContractChatSessionId { get; set; }
        [Column(TypeName = "varchar(4000)")]
        public string Message { get; set; }
        public int MsgType { get; set; }
        public virtual User SenderUser { get; set; }
        public virtual ContractChatSession ContractChatSession { get; set; }
    }
}
