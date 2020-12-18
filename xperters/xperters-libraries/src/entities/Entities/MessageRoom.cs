using System;
using System.Collections.Generic;

namespace xperters.entities.Entities
{
    public class MessageRoom : BaseEntity
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public virtual Job Job { get; set; }
        public ICollection<JobBidMessages> JobBidMessage { get; set; }
        public ICollection<RoomUser> RoomUsers { get; set; }
       

    }
}
