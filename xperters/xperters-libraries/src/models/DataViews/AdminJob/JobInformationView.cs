using System;
using System.Collections.Generic;

namespace xperters.models.DataViews.AdminJob
{
    public class JobInformationView
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime Created { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string Freelancer { get; set; }
        public DateTime ActiveDate { get; set; }
        public int NumberOfMilestones { get; set; }
        public IEnumerable<MilestoneView> MilestoneDetails { get; set; }
    }
}
