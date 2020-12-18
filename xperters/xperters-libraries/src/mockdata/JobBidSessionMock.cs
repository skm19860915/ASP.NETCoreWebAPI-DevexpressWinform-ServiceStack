using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
   public class JobBidSessionMock
    {
        public static Guid JobBidChatSessionsId1;
        public static Guid JobBidChatSessionsId2;

        private static readonly List<JobBidChatSessionDto> _JobBidChatSessions;

        public static JobBidChatSessionDto JobBidChatSessions1 => Get()[0];
        public static JobBidChatSessionDto JobBidChatSessions2 => Get()[1];

        static JobBidSessionMock()
        {

         JobBidChatSessionsId1 = Guid.Parse("{90000000-0000-0000-0000-000000000001}");
         JobBidChatSessionsId2 = Guid.Parse("{90000000-0000-0000-0000-000000000002}");


            _JobBidChatSessions = new List<JobBidChatSessionDto>
            {
                new JobBidChatSessionDto
                {
                    Id = JobBidChatSessionsId1
                },
                new JobBidChatSessionDto
                {
                    Id = JobBidChatSessionsId2
                }
            };
        }
        public static List<JobBidChatSessionDto> Get()
        {
            return _JobBidChatSessions;
        }
    }
}
