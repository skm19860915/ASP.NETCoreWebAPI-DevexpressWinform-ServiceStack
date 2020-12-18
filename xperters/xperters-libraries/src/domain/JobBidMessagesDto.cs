using System;

namespace xperters.domain
{
 public class JobBidMessagesDto:BaseDto
    {
        public Guid JobBidId { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; }
        public UserDto User { get; set; }
        public bool MsgType { get; set; }
        public Guid MessageRoomId { get; set; }
        public MessageRoomDto MessageRoom { get; set; }
    }
}
