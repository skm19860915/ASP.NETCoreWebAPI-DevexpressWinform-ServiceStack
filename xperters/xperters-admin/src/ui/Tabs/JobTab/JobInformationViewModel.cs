using System;
using System.ComponentModel;
using xperters.domain;

namespace Xperters.Admin.UI.Tabs.JobTab
{
    class JobInformationViewModel
    {
        public Guid JobId { get; set; }
        public DateTime SortByDate { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public DateTime Created { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string Freelancer { get; set; }
        public DateTime ActiveDate { get; set; }
        public int NumberOfMilestones { get; set; }
        public BindingList<MilestoneDetailDto> MilestoneInformations { get; set; }
    }
}
