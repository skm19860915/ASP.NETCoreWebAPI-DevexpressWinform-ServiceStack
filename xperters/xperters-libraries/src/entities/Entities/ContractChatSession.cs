using System;
using System.Collections.Generic;

namespace xperters.entities.Entities
{
  public class ContractChatSession:BaseEntity
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public virtual Job Job { get; set; }
        public virtual User Freelancer { get; set; }
        public virtual User Client { get; set; }
        public ICollection<ContractChatMessage> ContractChatMessages { get; set; }
        public ICollection<ContractChatSessionUser> ContractChatSessionUsers { get; set; }
    }
}
