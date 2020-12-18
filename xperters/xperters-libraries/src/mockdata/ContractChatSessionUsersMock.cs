using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
    public class ContractChatSessionUsersMock
    {
        public static Guid ContractChatSessionUsersId1;
        public static Guid ContractChatSessionUsersId2;
        //public static Guid ContractChatSessionUsersId3;
        //public static Guid ContractChatSessionUsersId4;
        //public static Guid ContractChatSessionUsersId5;

        private static readonly List<ContractChatSessionUserDto> _ContractChatSessionUsers;

        public static ContractChatSessionUserDto ContractChatSessionUsers1 => Get()[0];
        public static ContractChatSessionUserDto ContractChatSessionUsers2 => Get()[1];
        //public static ContractChatSessionUserDto ContractChatSessionUsers3 => Get()[2];
        //public static ContractChatSessionUserDto ContractChatSessionUsers4 => Get()[3];
        //public static ContractChatSessionUserDto ContractChatSessionUsers5 => Get()[4];

        static ContractChatSessionUsersMock()
        {
            ContractChatSessionUsersId1 = Guid.Parse("{80000000-0000-0000-0000-000000000001}");
            ContractChatSessionUsersId2 = Guid.Parse("{80000000-0000-0000-0000-000000000002}");
            //ContractChatSessionUsersId3 = Guid.Parse("{80000000-0000-0000-0000-000000000003}");
            //ContractChatSessionUsersId4 = Guid.Parse("{80000000-0000-0000-0000-000000000004}");
            //ContractChatSessionUsersId5 = Guid.Parse("{80000000-0000-0000-0000-000000000005}");

            _ContractChatSessionUsers = new List<ContractChatSessionUserDto>
            {
                new ContractChatSessionUserDto
                {
                    Id = ContractChatSessionUsersId1
                },
                new ContractChatSessionUserDto
                {
                    Id = ContractChatSessionUsersId2
                }
        };
        }
        public static List<ContractChatSessionUserDto> Get()
        {
            return _ContractChatSessionUsers;
        }
    }
}