using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public class MilestoneMessages
    {
        public static Guid MilestoneMessageId1;
        public static Guid MilestoneMessageId2;
        public static Guid MilestoneMessageId3;

        private static readonly List<MilestoneMessageDto> MilestoneMessageDtos;
        public static MilestoneMessageDto MilestoneMessage1 => Get()[0];
        public static MilestoneMessageDto MilestoneMessage2 => Get()[1];
        public static MilestoneMessageDto MilestoneMessage3 => Get()[2];

        static MilestoneMessages()
        {
            MilestoneMessageId1 = Guid.Parse("{81000000-0000-0000-0000-000000000001}");
            MilestoneMessageId2 = Guid.Parse("{81000000-0000-0000-0000-000000000002}");
            MilestoneMessageId3 = Guid.Parse("{81000000-0000-0000-0000-000000000003}");

            MilestoneMessageDtos = new List<MilestoneMessageDto>
            {
                new MilestoneMessageDto
                {
                   MilestoneId = Milestones.MilestoneId1,
                   Description="MilestoneMessage1",
                },
                new MilestoneMessageDto
                {
                   MilestoneId = Milestones.MilestoneId1,
                   Description="MilestoneMessage2",
                },
                new MilestoneMessageDto
                {
                   MilestoneId = Milestones.MilestoneId2,
                   Description="MilestoneMessage3",
                }
            };
        }

        public static List<MilestoneMessageDto> Get()
        {
            return MilestoneMessageDtos;
        }
    }
}
