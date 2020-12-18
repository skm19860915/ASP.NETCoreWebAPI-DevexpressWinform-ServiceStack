using System;
using System.Collections.Generic;
using Bogus;
using Humanizer;
using xperters.mockdata.Extensions;
using xperters.domain;
using xperters.mockdata.Mapping;

namespace xperters.mockdata
{
    public static class Jobs
    {
        private static readonly List<JobDto> JobsList;

        public static JobDto Job1 => Get()[0];
        public static JobDto Job2 => Get()[1];
        public static JobDto Job3 => Get()[2];
        public static JobDto Job4 => Get()[3];
        public static JobDto Job5 => Get()[4];
        public static JobDto Job6 => Get()[5];
        public static JobDto Job7 => Get()[6];
        public static JobDto Job8 => Get()[7];
        public static JobDto Job9 => Get()[8];
        public static JobDto Job10 => Get()[9];
        public static JobDto Job11 => Get()[10];
        public static JobDto Job12 => Get()[11];
        public static JobDto Job13 => Get()[12];
        public static JobDto Job16 => Get()[15];
        public static JobDto Job17 => Get()[16];
        public static JobDto Job18 => Get()[17];
        public static JobDto Job19 => Get()[18];
        public const int JobsCount = 500;        
        
        static Jobs()
        {
            var jobId = Guid.Parse("{10000000-0000-0000-0000-000000000000}");
            
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(Users.RandomSeed);
            var clients = Users.Clients;
            
            var jobsFakes = new Faker<JobDto>()
                .RuleFor(o => o.Id, f =>
                {
                    var oldguid = jobId;
                    jobId = jobId.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.UserId, f=> f.PickRandom(clients).Id)
                .RuleFor(o => o.JobTitle, f => f.Lorem.Sentence().Truncate(254).TrimEnd())
                .RuleFor(o => o.Description, f => f.Lorem.Paragraph())
                .RuleFor(o => o.EstimatedBudgetId, f => f.Random.Int(1,10))
                .RuleFor(o => o.CreatedDate, f => f.Date.Recent())
                .RuleFor(o => o.ModifiedDate, (f, u) => u.CreatedDate.AddHours(f.Random.Int(0,100)).AddMilliseconds(f.Random.Int(0,100000)))
                .RuleFor(o => o.ExperienceLevel, f => 3)
                .RuleFor(o => o.FreelancerTypeId, f => f.Random.Int(1,2))
                .RuleFor(o => o.FreelancersStrength, f => f.Random.Int(1,2))
                .RuleFor(o => o.JobDuration, f => f.Random.Int(2,60))
                .RuleFor(o => o.JobPrice, f => f.Random.Decimal(100,5000))
                .RuleFor(o => o.JobStatusId, f => f.Random.Int(1,5))
                .RuleFor(o => o.JobVisibility, f => 1)
                .RuleFor(o => o.JobStatusId, f => 1)
                .RuleFor(o => o.JobTypeId, f => f.Random.Int(1,3))
                .RuleFor(o => o.PaymentTypeId, f => 1)
                .RuleFor(o => o.SelectedJobCategory, f => f.Random.Int(1,22))
                .RuleFor(r=>r.Id, f =>
                {
                    jobId = jobId.Increment();
                    return jobId;
                });

            JobsList = jobsFakes.Generate(JobsCount);            
        }
        public static List<JobDto> Get()
        {
            return JobsList;
        }
    }
}
