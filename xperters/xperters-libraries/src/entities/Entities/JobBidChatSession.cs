using System;
using System.Collections.Generic;

namespace xperters.entities.Entities
{
    public class JobBidChatSession : BaseEntity
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public virtual Job Job { get; set; }
        public virtual User Freelancer { get; set; }
        public virtual User Client { get; set; }

        public ICollection<JobBidChatMessage> JobBidChatMessages { get; set; }
        public ICollection<JobBidChatSessionUser> JobBidChatSessionUsers { get; set; }
       

    }
}
