using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
  public class JobBidChatMessagesMock
    {                     
        public static Guid JobBidChatMessagesId1;
        public static Guid JobBidChatMessagesId2;
        public static Guid JobBidChatMessagesId3;
        public static Guid JobBidChatMessagesId4;
        public static Guid JobBidChatMessagesId5;

        private static readonly List<JobBidChatMessageDto> _jobbidChatMessages;
                                            
        public static JobBidChatMessageDto jobBidChatMessages1 => Get()[0];
        public static JobBidChatMessageDto jobBidChatMessages2 => Get()[1];
        public static JobBidChatMessageDto jobBidChatMessages3 => Get()[2];
        public static JobBidChatMessageDto jobBidChatMessages4 => Get()[3];
        public static JobBidChatMessageDto jobBidChatMessages5 => Get()[4];

        static JobBidChatMessagesMock()
        {
            JobBidChatMessagesId1 = Guid.Parse("{90000000-0000-0000-0000-000000000006}");
            JobBidChatMessagesId2 = Guid.Parse("{90000000-0000-0000-0000-000000000007}");
            JobBidChatMessagesId3 = Guid.Parse("{90000000-0000-0000-0000-000000000008}");
            JobBidChatMessagesId4 = Guid.Parse("{90000000-0000-0000-0000-000000000009}");
            JobBidChatMessagesId5 = Guid.Parse("{90000000-0000-0000-0000-000000000000}");

            _jobbidChatMessages = new List<JobBidChatMessageDto>
            {
                new JobBidChatMessageDto
                {
                    Id = JobBidChatMessagesId1,
                    Message ="Hello",
                    MessageType=2
                 },
                new JobBidChatMessageDto
                {
                    Id = JobBidChatMessagesId2,
                    Message ="abc 1232",
                    MessageType=2,
                },
                new JobBidChatMessageDto
                {
                    Id = JobBidChatMessagesId3,
                    Message ="Hello New Text",
                    MessageType=3
                },
                new JobBidChatMessageDto
                {
                    Id = JobBidChatMessagesId4,
                    Message ="text",
                    MessageType=3
                },
                new JobBidChatMessageDto
                {
                    Id = JobBidChatMessagesId5,
                    Message ="Welcome",
                    MessageType=2
                },
        };
    }
        public static List<JobBidChatMessageDto> Get()
        {
            return _jobbidChatMessages;
        }
    }
}
