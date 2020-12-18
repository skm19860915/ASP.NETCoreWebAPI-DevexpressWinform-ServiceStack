using System;
using System.ComponentModel.DataAnnotations;

namespace xperters.entities.Entities
{
    public class RoomUser : BaseEntity
    {
        [Required]
        public Guid MessageRoomId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual MessageRoom MessageRoom { get; set; }
        public virtual User User { get; set; }

    }
}
