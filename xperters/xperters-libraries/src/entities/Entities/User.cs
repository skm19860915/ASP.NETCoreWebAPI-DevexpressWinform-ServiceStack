using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using xperters.enums;

namespace xperters.entities.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string MobilePhone { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string LastName { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string DisplayName { get; set; }
        
        [Column(TypeName = "varchar(1024)")]
        public string Avatar { get; set; }        

        public Enums.UserRole UserRole { get; set; }

        public bool IsEnabled { get; set; }

        public virtual int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<JobBid> JobBids { get; set; }
        public virtual ICollection<JobBidChatSession> JobBidChatSessionClients { get; set; }
        public virtual ICollection<JobBidChatSession> JobBidChatSessionsFreelancers { get; set; }
        public virtual ICollection<JobBidChatSessionUser> JobBidChatSessionUsers { get; set; }
        public virtual ICollection<ContractChatMessage> ContractChatMessages { get; set; }
        public virtual ICollection<ContractChatSession> ContractChatSessionClients { get; set; }
        public virtual ICollection<ContractChatSession> ContractChatSessionsFreelancers { get; set; }
        public virtual ICollection<ContractChatSessionUser> ContractChatSessionUsers { get; set; }
        public virtual ICollection<JobContract> JobContracts { get; set; }
        public virtual ICollection<Milestone> Milestones { get; set; }
        public virtual ICollection<ContractMilestoneFund> ContractFunds { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<EmailAudit> EmailAudits { get; set; }
        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
        public virtual ICollection<UserBalance> UserBalances { get; set; }
        public virtual ICollection<UserPayment> UserPayments { get; set; }
        public virtual ICollection<UserWithdrawal> UserWithdrawals { get; set; }
    }
}