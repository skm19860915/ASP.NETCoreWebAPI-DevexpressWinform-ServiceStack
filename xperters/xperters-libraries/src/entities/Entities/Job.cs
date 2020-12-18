using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class Job : BaseEntity
    {
        public Guid UserId { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Required]
        public string JobTitle { get; set; }
        public int SelectedJobCategory { get; set; }

        [Required]
        [Column(TypeName = "varchar(4000)")]
        public string Description { get; set; }
        public int JobTypeId { get; set; }
        public int JobVisibility { get; set; }
        public int FreelancersStrength { get; set; }
        public int PaymentTypeId { get; set; }
        public int ExperienceLevel { get; set; }
        public int JobDuration { get; set; }
        public int FreelancerTypeId { get; set; } //FullTime/PartTime
        public int ClientHistory { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal JobPrice { get; set; }
        public User User { get; set; }
        public bool IsDraft { get; set; }

        public int JobStatusId { get; set; }
        public int EstimatedBudgetId { get; set; }

        [ForeignKey("JobStatusId")]
        public JobStatus JobStatus { get; set; }
        public ICollection<JobBidChatSession> JobBidChatSessions { get; set; }
        public ICollection<ContractChatSession> ContractChatSessions { get; set; }
        public ICollection<JobAttachment> JobAttachments { get; set; }
        public ICollection<JobBid> JobBids { get; set; }
    }
}
