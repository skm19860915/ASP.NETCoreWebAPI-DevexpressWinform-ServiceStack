using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
  public  class JobBidChatSessionUsersMock
    {
        public static Guid JobBidChatSessionUserId1;
        public static Guid JobBidChatSessionUserId2;

        private static readonly List<JobBidChatSessionUsersDto> _jobBidChatSessionUsers;

        public static JobBidChatSessionUsersDto JobBidChatSessionUser1 => Get()[0];
        public static JobBidChatSessionUsersDto JobBidChatSessionUser2 => Get()[1];

        static JobBidChatSessionUsersMock()
        {
            JobBidChatSessionUserId1 = Guid.Parse("{70000000-0000-0000-0000-000000000003}");
            JobBidChatSessionUserId1 = Guid.Parse("{70000000-0000-0000-0000-000000000004}");


            _jobBidChatSessionUsers = new List<JobBidChatSessionUsersDto>
            {
                new JobBidChatSessionUsersDto
                {
                    Id = JobBidChatSessionUserId1
                },
                new JobBidChatSessionUsersDto
                {
                    Id = JobBidChatSessionUserId2
                }
            };
        }
        public static List<JobBidChatSessionUsersDto> Get()
        {
            return _jobBidChatSessionUsers;
        }
    }
}
