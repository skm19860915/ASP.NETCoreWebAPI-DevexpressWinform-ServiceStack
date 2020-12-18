using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;
using xperters.enums;

namespace xperters.mockdata
{
    public static class Contract
    {
        public static Guid ContractId1;
        public static Guid ContractId2;
        public static Guid ContractId3;
        public static Guid ContractId4;
        public static Guid ContractId5;


        private static readonly List<HiredJobDto> _HiredJobDto;
        public static HiredJobDto HiredJob1 => Get()[0];
        public static HiredJobDto HiredJob2 => Get()[1];
   

        static Contract()
        {
            ContractId1 = Guid.Parse("{60000000-0000-0000-0000-000000000001}");
            ContractId2 = Guid.Parse("{60000000-0000-0000-0000-000000000002}");
           
            _HiredJobDto = new List<HiredJobDto>
            {
                new HiredJobDto
                {
                   Id=ContractId1,
                   Message ="Contract1",
                   messageType =1,
                   ContractChatSessionId=new Guid(),
                   Amount=100,
                   ContractStatus=Enums.ContractStatus.ContractStart.GetEnumValue()
                },
                new HiredJobDto
                {
                   Id=ContractId2,
                   Message="Contract2",
                   messageType=1,
                   ContractChatSessionId=new Guid(),
                   Amount=200,
                   ContractStatus=Enums.ContractStatus.ContractStart.GetEnumValue(),
                }

            };
        }
        public static List<HiredJobDto> Get()
        {
            return _HiredJobDto;
        }
    }
}
