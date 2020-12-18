using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public static class JobInfos
    {
        public static string JobInfoGuid1 = "{12300000-0000-0000-0000-000000000001}";
        public static string JobInfoGuid2 = "{12300000-0000-0000-0000-000000000002}";
        public static string JobInfoGuid3 = "{12300000-0000-0000-0000-000000000003}";
        public static string JobInfoGuid4 = "{12300000-0000-0000-0000-000000000004}";
        public static string JobInfoGuid5 = "{12300000-0000-0000-0000-000000000005}";
        public static string JobInfoGuid6 = "{12300000-0000-0000-0000-000000000006}";
        public static string JobInfoGuid7 = "{12300000-0000-0000-0000-000000000007}";
        public static string JobInfoGuid8 = "{12300000-0000-0000-0000-000000000008}";
        public static string JobInfoGuid9 = "{12300000-0000-0000-0000-000000000009}";
        public static string JobInfoGuid10 = "{12300000-0000-0000-0000-000000000010}";
        public static Guid JobInfoId1;
        public static Guid JobInfoId2;
        public static Guid JobInfoId3;
        public static Guid JobInfoId4;
        public static Guid JobInfoId5;
        public static Guid JobInfoId6;
        public static Guid JobInfoId7;
        public static Guid JobInfoId8;
        public static Guid JobInfoId9;
        public static Guid JobInfoId10;


        private static readonly List<JobInformationDto> _JobInformationDto;
        public static JobInformationDto JobInformation1 => Get()[0];
        public static JobInformationDto JobInformation2 => Get()[1];


        static JobInfos()
        {
            JobInfoId1 = Guid.Parse(JobInfoGuid1);
            JobInfoId2 = Guid.Parse(JobInfoGuid2);
            JobInfoId3 = Guid.Parse(JobInfoGuid3);
            JobInfoId4 = Guid.Parse(JobInfoGuid4);
            JobInfoId5 = Guid.Parse(JobInfoGuid5);
            JobInfoId6 = Guid.Parse(JobInfoGuid6);
            JobInfoId7 = Guid.Parse(JobInfoGuid7);
            JobInfoId8 = Guid.Parse(JobInfoGuid8);
            JobInfoId9 = Guid.Parse(JobInfoGuid9);
            JobInfoId10 = Guid.Parse(JobInfoGuid10);

            _JobInformationDto = new List<JobInformationDto>
            {
                new JobInformationDto
                {
                    JobId = Jobs.Job1.Id,
                    JobTitle = Jobs.Job1.JobTitle,
                    JobDescription = Jobs.Job1.Description,
                    Created = Jobs.Job1.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },
                new JobInformationDto
                {
                    JobId = Jobs.Job2.Id,
                    JobTitle = Jobs.Job2.JobTitle,
                    JobDescription = Jobs.Job2.Description,
                    Created = Jobs.Job2.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job3.Id,
                    JobTitle = Jobs.Job3.JobTitle,
                    JobDescription = Jobs.Job3.Description,
                    Created = Jobs.Job3.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job4.Id,
                    JobTitle = Jobs.Job4.JobTitle,
                    JobDescription = Jobs.Job4.Description,
                    Created = Jobs.Job4.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job5.Id,
                    JobTitle = Jobs.Job5.JobTitle,
                    JobDescription = Jobs.Job5.Description,
                    Created = Jobs.Job5.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job6.Id,
                    JobTitle = Jobs.Job6.JobTitle,
                    JobDescription = Jobs.Job6.Description,
                    Created = Jobs.Job6.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job7.Id,
                    JobTitle = Jobs.Job7.JobTitle,
                    JobDescription = Jobs.Job7.Description,
                    Created = Jobs.Job7.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job8.Id,
                    JobTitle = Jobs.Job8.JobTitle,
                    JobDescription = Jobs.Job8.Description,
                    Created = Jobs.Job8.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job10.Id,
                    JobTitle = Jobs.Job10.JobTitle,
                    JobDescription = Jobs.Job10.Description,
                    Created = Jobs.Job10.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },                
                new JobInformationDto
                {
                    JobId = Jobs.Job9.Id,
                    JobTitle = Jobs.Job9.JobTitle,
                    JobDescription = Jobs.Job9.Description,
                    Created = Jobs.Job9.CreatedDate,
                    Owner = Users.CustomerFirst.DisplayName,
                    Status = "Active",
                    Freelancer = Users.FreelancerFirst.DisplayName,
                    ActiveDate = DateTime.Today,
                    NumberOfMilestones = 1
                },
            };
        }
        public static List<JobInformationDto> Get()
        {
            return _JobInformationDto;
        }
    }
}