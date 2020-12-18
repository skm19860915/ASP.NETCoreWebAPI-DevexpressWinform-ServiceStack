using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
  public class ContractChatSessionsMock
    {
        public static Guid ContractChatSessionsId1;
        public static Guid ContractChatSessionsId2;
       
        private static readonly List<ContractChatSessionDto> _ContractChatSessions;

        public static ContractChatSessionDto ContractChatSessions1 => Get()[0];
        public static ContractChatSessionDto ContractChatSessions2 => Get()[1];
      

        static ContractChatSessionsMock()
        {
            ContractChatSessionsId1 = Guid.Parse("{70000000-0000-0000-0000-000000000001}");
            ContractChatSessionsId2 = Guid.Parse("{70000000-0000-0000-0000-000000000002}");
           
           

            _ContractChatSessions = new List<ContractChatSessionDto>
            {
                new ContractChatSessionDto
                {
                    Id = ContractChatSessionsId1
                },
                new ContractChatSessionDto
                {
                    Id = ContractChatSessionsId2
                }
            };
        }
        public static List<ContractChatSessionDto> Get()
        {
            return _ContractChatSessions;
        }
    }
}
