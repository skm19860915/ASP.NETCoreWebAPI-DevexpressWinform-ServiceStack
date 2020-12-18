using System;

namespace xperters.models
{
   public  class JobBidMessagesView:BaseView
    {
        public Guid JobBidId { get; set; }
        public Guid SenderId { get; set; }
        public JobBidView JobBidView { get; set; }
        public UserView UserView { get; set; }
        public  MessageRoomView MessageRoomView { get; set; }
        public string Message { get; set; }
        public bool MsgType { get; set; }
       
    }
}
