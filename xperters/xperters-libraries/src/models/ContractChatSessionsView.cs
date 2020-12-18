using System;
using System.Collections.Generic;

namespace xperters.models
{
  public  class ContractChatSessionsView : BaseView
    {
        public Guid JobId { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid ClientId { get; set; }
        public string Message { get; set; }
        public int messageType { get; set; }
        public Guid ContractChatSessionsId { get; set; }
        public List<ContractChatSessionUsersView> ContractChatSessionUsersView { get; set; }
        public JobView jobView { get; set; }
        public List<ContractChatMessagesView> ContractChatMessagesView { get; set; }
    }
}
