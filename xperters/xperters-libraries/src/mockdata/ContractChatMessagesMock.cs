using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
  public  class ContractChatMessagesMock
    {
        public static Guid ContractChatMessagesId1;
        public static Guid ContractChatMessagesId2;
        public static Guid ContractChatMessagesId3;
        public static Guid ContractChatMessagesId4;
        public static Guid ContractChatMessagesId5;

        private static readonly List<ContractChatMessageDto> _ContractChatMessages;
                                                 
        public static ContractChatMessageDto ContractChatMessages1 => Get()[0];
        public static ContractChatMessageDto ContractChatMessages2 => Get()[1];
        public static ContractChatMessageDto ContractChatMessages3 => Get()[2];
        public static ContractChatMessageDto ContractChatMessages4 => Get()[3];
        public static ContractChatMessageDto ContractChatMessages5 => Get()[4];

        static ContractChatMessagesMock()
        {
            ContractChatMessagesId1 = Guid.Parse("{90000000-0000-0000-0000-000000000001}");
            ContractChatMessagesId2 = Guid.Parse("{90000000-0000-0000-0000-000000000002}");
            ContractChatMessagesId3 = Guid.Parse("{90000000-0000-0000-0000-000000000003}");
            ContractChatMessagesId4 = Guid.Parse("{90000000-0000-0000-0000-000000000004}");
            ContractChatMessagesId5 = Guid.Parse("{90000000-0000-0000-0000-000000000005}");

            _ContractChatMessages = new List<ContractChatMessageDto>
            {
                new ContractChatMessageDto
                {
                    Id = ContractChatMessagesId1,
                    Message ="Hello",
                    MsgType=true
                 },
                new ContractChatMessageDto
                {
                    Id = ContractChatMessagesId2,
                    Message ="abc 1232",
                    MsgType=false
                },
                new ContractChatMessageDto
                {
                    Id = ContractChatMessagesId3,
                    Message ="Hello New Text",
                    MsgType=true
                },
                new ContractChatMessageDto
                {
                    Id = ContractChatMessagesId4,
                    Message ="text",
                    MsgType=false
                },
                new ContractChatMessageDto
                {
                    Id = ContractChatMessagesId5,
                    Message ="Welcome",
                    MsgType=true
                },
        };
}
        public static List<ContractChatMessageDto> Get()
        {
            return _ContractChatMessages;
        }
    }
}
