using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
    public class MilestoneStatusMock
    {
        private static readonly List<MilestoneStatusDto> MileStoneStatusDto;
        public static MilestoneStatusDto MileStoneStatus1 => Get()[0];
        public static MilestoneStatusDto MileStoneStatus2 => Get()[1];
        public static MilestoneStatusDto MileStoneStatus3 => Get()[2];
        public static MilestoneStatusDto MileStoneStatus4 => Get()[3];
        public static MilestoneStatusDto MileStoneStatus5 => Get()[4];
        public static MilestoneStatusDto MileStoneStatus6 => Get()[5];
        static MilestoneStatusMock()
        {
            MileStoneStatusDto = new List<MilestoneStatusDto>
            {
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 1,
                    StatusDescription = "Add Funds",
                    IsActive = true
                },
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 2,
                    StatusDescription = "Active",
                    IsActive = true
                },
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 3,
                    StatusDescription = "Freelancer Closed. Waiting For ClientApproval",
                    IsActive = true
                },
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 4,
                    StatusDescription = "Admin Approved",
                    IsActive = true
                },
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 5,
                    StatusDescription = "Paid",
                    IsActive = true
                },
                new MilestoneStatusDto
                {
                    MilestoneStatusId = 6,
                    StatusDescription = "Cancelled",
                    IsActive = true
                }
            };
        }
        public static List<MilestoneStatusDto> Get()
        {
            return MileStoneStatusDto;
        }
    }
}
