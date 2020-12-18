using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;
using xperters.enums;

namespace xperters.mockdata
{
   public class EmailAuditMock
    {
        public static Guid EmailAuditId1;
        public static Guid EmailAuditId2;
        public static Guid EmailAuditId3;

        private static readonly List<EmailAuditDto> _emailAuditMocks;

        public static EmailAuditDto EmailAudit1 => Get()[0];
        public static EmailAuditDto EmailAudit2 => Get()[1];
        public static EmailAuditDto EmailAudit3 => Get()[2];
        static EmailAuditMock()
        {
            EmailAuditId1 = Guid.Parse("{90000000-0000-0000-0000-000000000011}");
            EmailAuditId2 = Guid.Parse("{90000000-0000-0000-0000-000000000012}");
            EmailAuditId3 = Guid.Parse("{90000000-0000-0000-0000-000000000013}");

            _emailAuditMocks = new List<EmailAuditDto>
            {
                new EmailAuditDto
                {
                    Id = EmailAuditId1,
                    SenderEmailAddress =Users.CustomerFirst.Email,
                    ReceiverEmailAddress=Users.FreelancerFirst.Email,
                    ReceiverId=Users.CustomerFirst.Id,
                    SenderId=Users.FreelancerFirst.Id,
                    Content="You receive a bid for job."
                 },
                new EmailAuditDto
                {
                    Id = EmailAuditId2,
                   SenderEmailAddress =Users.CustomerSecond.Email,
                    ReceiverEmailAddress=Users.FreelancerSecond.Email,
                    ReceiverId=Users.CustomerSecond.Id,
                    SenderId=Users.FreelancerSecond.Id,
                     Content="You receive a bid for job."
                },
                 new EmailAuditDto
                {
                    Id = EmailAuditId3,
                   SenderEmailAddress =Users.CustomerThird.Email,
                    ReceiverEmailAddress=Users.FreelancerSecond.Email,
                    ReceiverId=Users.CustomerThird.Id,
                    SenderId=Users.FreelancerSecond.Id,
                     Content="You receive a bid for job."
                }
            };

        }
        public static List<EmailAuditDto> Get()
        {
            return _emailAuditMocks;
        }
    }
    }
