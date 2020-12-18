using System;
using System.Collections.Generic;

namespace xperters.models
{
    public class JobView
    {
        public JobView()
        {
            JobAttachments = new List<JobAttachmentView>(5);
            CreatedDate = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string JobTitle { get; set; }
        public int SelectedJobCategory { get; set; }
        public string Description { get; set; }
        public int JobTypeId { get; set; }
        public int JobVisibility { get; set; }
        public int FreelancersStrength { get; set; }
        public int PaymentTypeId { get; set; }
        public int ExperienceLevel { get; set; }
        public int JobDuration { get; set; }
        public int FreelancerTypeId { get; set; } //FullTime/PartTime
        public int ClientHistory { get; set; }
        public decimal JobPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDraft { get; set; }
        public int JobStatusId { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string FreelancerExperience { get; set; }
        public string FreelancerType { get; set; }
        public string JobType { get; set; }
        public int EstimatedBudgetId { get; set; }
        public string EstimatedBudget { get; set; }
        public List<MessageRoomView> MessageRoomViews { get; set; }
        public List<JobAttachmentView> JobAttachments { get; set; }
        public List <JobBidView> JobBidView { get; set; }
        public int PostJobCount { get; set; }
        public int InprogressJobCount { get; set; }
        public int CompletedJobCount { get; set; }
        public int CanceledJobCount { get; set; }

    }
}
