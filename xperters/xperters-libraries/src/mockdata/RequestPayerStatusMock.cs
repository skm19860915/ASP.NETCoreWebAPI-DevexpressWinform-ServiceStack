using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public class RequestPayerStatusMock
    {
        private static readonly List<RequestPayerStatusDto> RequestPayerStatusDto;
        public static RequestPayerStatusDto RequestPayerStatus1 => Get()[0];
        public static RequestPayerStatusDto RequestPayerStatus2 => Get()[1];
        public static RequestPayerStatusDto RequestPayerStatus3 => Get()[2];
        public static RequestPayerStatusDto RequestPayerStatus4 => Get()[3];
        static RequestPayerStatusMock()
        {
            RequestPayerStatusDto = new List<RequestPayerStatusDto>
            {
                new RequestPayerStatusDto
                {
                    PayerStatusId = 1,
                    PayerStatus = "Successful",
                    IsActive = true
                },
                new RequestPayerStatusDto
                {
                    PayerStatusId = 2,
                    PayerStatus = "Pending",
                    IsActive = true
                },
                new RequestPayerStatusDto
                {
                    PayerStatusId = 3,
                    PayerStatus = "Cancelled",
                    IsActive = true
                },
                new RequestPayerStatusDto
                {
                    PayerStatusId = 4,
                    PayerStatus = "Failed",
                    IsActive = true
                }
            };
        }
        public static List<RequestPayerStatusDto> Get()
        {
            return RequestPayerStatusDto;
        }
    }
}
