using System;
using System.Collections.Generic;

namespace xperters.domain
{
    public class JobDto : BaseDto
    {
        public JobDto()
        {
            JobAttachments = new List<JobAttachmentDto>(5);
        }


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
        public int FreelancerTypeId { get; set; }
        public int ClientHistory { get; set; }
        public decimal JobPrice { get; set; }
        public bool IsDraft { get; set; }
        public int JobStatusId { get; set; }
        public int EstimatedBudgetId { get; set; }
        public List<JobBidChatSessionDto> JobBidChatSessions { get; set; }
        public List<JobAttachmentDto> JobAttachments { get; set; }
        public List<JobBidDto> JobBids { get; set; }
        public JobStatusDto JobStatus { get; set; }

        public int PostJobCount { get; set; }
        public int InprogressJobCount { get; set; }
        public int CompletedJobCount { get; set; }
        public int CanceledJobCount { get; set; }
    }
}
