using System;

namespace xperters.models
{
 public  class ContractChatMessagesView
    {
        public Guid SenderId { get; set; }
        public Guid ContractChatSessionsId { get; set; }
        public string Message { get; set; }
        public bool MsgType { get; set; }
        public UserView UserView { get; set; }
        public ContractChatSessionsView ContractChatSessionsView { get; set; }
    }
}
