using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public class JobStatusMock
    {
        private static readonly List<JobStatusDto> JobStatusDtos;

        public static JobStatusDto JobStatusPosted => Get()[0];
        public static JobStatusDto JobStatusContractSigned => Get()[1];
        public static JobStatusDto JobStatusInProgress => Get()[2];
        public static JobStatusDto JobStatusCompleted => Get()[3];
        public static JobStatusDto JobStatusCanceled => Get()[4];
        static JobStatusMock()
        {
            JobStatusDtos = new List<JobStatusDto>
            {
                new JobStatusDto
                {
                    JobStatusId = 1,
                    Status = "Job Posted",
                    IsActive = true
                },
                new JobStatusDto
                {
                    JobStatusId = 2,
                    Status = "ContractSigned",
                    IsActive = true
                },
                new JobStatusDto
                {
                    JobStatusId = 3,
                    Status = "Job In-Progress",
                    IsActive = true
                },
                new JobStatusDto
                {
                    JobStatusId = 4,
                    Status = "Job Completed",
                    IsActive = true
                },
                new JobStatusDto
                {
                    JobStatusId = 5,
                    Status = "Job Canceled",
                    IsActive = true
                }
            };
        }
        public static List<JobStatusDto> Get()
        {
            return JobStatusDtos;
        }
    }
}
