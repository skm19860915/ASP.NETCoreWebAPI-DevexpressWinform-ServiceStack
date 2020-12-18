using System;
using System.Collections.Generic;
using xperters.domain;
using xperters.enums;

namespace xperters.mockdata
{
    public static class JobBids
    {
        public static Guid BidId1;
        public static Guid BidId2;
        public static Guid BidId3;
        public static Guid BidId4;
        public static Guid BidId5;
        public static Guid BidId6;
        public static Guid BidId7;
        public static Guid BidId8;
        public static Guid BidId9;
        public static Guid BidId10;
        public static Guid BidId11;
        public static Guid BidId12;

        private static readonly List<JobBidDto> _jobBids;

        public static JobBidDto JobBid1 => Get()[0];
        public static JobBidDto JobBid2 => Get()[1];
        public static JobBidDto JobBid3 => Get()[2];
        public static JobBidDto JobBid4 => Get()[3];
        public static JobBidDto JobBid5 => Get()[4];
        public static JobBidDto JobBid6 => Get()[5];
        public static JobBidDto JobBid7 => Get()[6];
        public static JobBidDto JobBid8 => Get()[7];
        public static JobBidDto JobBid9 => Get()[8];
        public static JobBidDto JobBid10 => Get()[9];
        public static JobBidDto JobBid11 => Get()[10];
        public static JobBidDto JobBid12 => Get()[11];

        static JobBids()
        {
            BidId1 = Guid.Parse("{50000000-0000-0000-0000-000000000001}");
            BidId2 = Guid.Parse("{50000000-0000-0000-0000-000000000002}");
            BidId3 = Guid.Parse("{50000000-0000-0000-0000-000000000003}");
            BidId4 = Guid.Parse("{50000000-0000-0000-0000-000000000004}");
            BidId5 = Guid.Parse("{50000000-0000-0000-0000-000000000005}");
            BidId6 = Guid.Parse("{50000000-0000-0000-0000-000000000006}");
            BidId7 = Guid.Parse("{50000000-0000-0000-0000-000000000007}");
            BidId8 = Guid.Parse("{50000000-0000-0000-0000-000000000008}");
            BidId9 = Guid.Parse("{50000000-0000-0000-0000-000000000009}");
            BidId10 = Guid.Parse("{50000000-0000-0000-0000-000000000010}");
            BidId11 = Guid.Parse("{50000000-0000-0000-0000-000000000011}");
            BidId12 = Guid.Parse("{50000000-0000-0000-0000-000000000012}");

            _jobBids = new List<JobBidDto>
            {
                new JobBidDto
                {
                    Message = "Bid 1",
                    Id = BidId1,
                    BidAmount = 1,
                    BidStatus=(int)JobEnums.JobBidStatus.BidSelected

                },
                new JobBidDto
                {
                    Message = "Bid 2",
                    Id = BidId2,
                    BidAmount = 2,
                    BidStatus=(int)JobEnums.JobBidStatus.BidSelected

                },
                new JobBidDto
                {
                    Message = "Bid 3",
                    Id = BidId3,
                    BidAmount = 1,
                    BidStatus=(int)JobEnums.JobBidStatus.BidAmendment
                },
                new JobBidDto
                {
                    Message = "Bid 4",
                    Id = BidId4,
                    BidAmount = 4,
                    BidStatus=(int)JobEnums.JobBidStatus.BidAmendment
                },
                new JobBidDto
                {
                    Message = "Bid 5",
                    Id = BidId5,
                    BidAmount = 5,
                    BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 6",
                    Id = BidId6,
                    BidAmount = 6,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 7",
                    Id = BidId7,
                    BidAmount = 7,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 8",
                    Id = BidId8,
                    BidAmount = 8,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 9",
                    Id = BidId9,
                    BidAmount = 9,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 10",
                    Id = BidId10,
                    BidAmount = 10,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 11",
                    Id = BidId11,
                    BidAmount = 11,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                },
                new JobBidDto
                {
                    Message = "Bid 12",
                    Id = BidId12,
                    BidAmount = 12,
                     BidStatus=(int)JobEnums.JobBidStatus.BidsSubmitted
                }
            };
        }

        public static List<JobBidDto> Get()
        {
            return _jobBids;
        }
    }
}
