using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using xperters.entities.Entities;
using xperters.enums;

namespace xperters.entities
{
    public class XpertersContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobAttachment> JobAttachments { get; set; }
        public DbSet<JobBid> JobBids { get; set; }
        public DbSet<JobBidAttachment> JobBidAttachments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<JobBidChatSession> JobBidChatSessions { get; set; }
        public DbSet<JobBidChatMessage> JobBidChatMessages { get; set; }
        public DbSet<JobBidChatSessionUser> JobBidChatSessionUsers { get; set; }
        public DbSet<ContractChatMessage> ContractChatMessages { get; set; }
        public DbSet<ContractChatSession> ContractChatSessions { get; set; }
        public DbSet<ContractChatSessionUser> ContractChatSessionUsers { get; set; }
        public DbSet<JobContract> JobContracts { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<ContractMilestoneFund> ContractMilestoneFunds { get; set; }
        public DbSet<MilestoneAttachment> MilestoneAttachments { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<EmailAudit> EmailAudits { get; set; }
        public DbSet<EmailAttachments> EmailAttachments { get; set; }
        public DbSet<AccountDetail> AccountDetails { get; set; }
        public DbSet<MilestoneMessage> MilestoneMessage { get; set; }
        public DbSet<MilestoneRequestPayer> MilestoneRequestPayers { get; set; }
        public DbSet<MilestoneSystemRequestPayer> MilestoneSystemRequestPayers { get; set; }
        public DbSet<JobStatus> JobStatus { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<RequestPayerStatus> RequestPayerStatus { get; set; }

        public DbSet<MilestoneStatus> MilestoneStatus { get; set; }

        public DbSet<SystemBalance> SystemBalances { get; set; }
        public DbSet<SystemPayment> SystemPayments { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<UserPayment> UserPayments { get; set; }
        public DbSet<UserWithdrawal> UserWithdrawals{ get; set; }
        public DbSet<FeeStructure> FeeStructures{ get; set; }
        public XpertersContext(DbContextOptions<XpertersContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE all data type have moved to the entity property names as attributes
            // PLEASE DO NOT ADD THEM HERE.

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasData(MasterDataFactory.GetCategoryData()
            );

            modelBuilder.Entity<Skill>()
                .ToTable("Skills")
                .HasData(MasterDataFactory.GetSkillData()
            );

            modelBuilder.Entity<Country>()
                .ToTable("Countries")
                .HasData(MasterDataFactory.GetCountryData());

            modelBuilder.Entity<Currency>()
                .ToTable("Currencies")
                .HasData(MasterDataFactory.GetCurrenciesData());

            modelBuilder.Entity<RequestPayerStatus>()
                .ToTable("RequestPayerStatus")
                .HasData(MasterDataFactory.GetRequestPayerStatusData());

            modelBuilder.Entity<RequestPayerStatus>()
                .HasMany(g => g.MilestoneRequestPayers);

            modelBuilder.Entity<JobAttachment>()
                .ToTable("JobAttachments");
            modelBuilder.Entity<JobBidChatSession>()
                .ToTable("JobBidChatSessions");
            modelBuilder.Entity<JobBidChatSessionUser>()
                .ToTable("JobBidChatSessionUsers");
            modelBuilder.Entity<JobBidChatMessage>()
                .ToTable("JobBidChatMessages");

            modelBuilder.Entity<ContractChatMessage>()
                .ToTable("ContractChatMessages");
            modelBuilder.Entity<ContractChatSession>()
                        .ToTable("ContractChatSessions")
                        .HasMany(r => r.ContractChatMessages)
                        .WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContractChatSessionUser>()
                        .ToTable("ContractChatSessionUsers");

            modelBuilder.Entity<JobContract>()
                        .ToTable("JobContracts");

            modelBuilder.Entity<Milestone>()
                .ToTable("Milestones")
                .HasMany(r => r.MilestoneRequestPayers)
                .WithOne(x => x.Milestone)
                .HasForeignKey(s => s.MilestoneId);

            modelBuilder.Entity<Milestone>()
                .ToTable("Milestones")
                .HasMany(r => r.MilestoneSystemRequestPayers)
                .WithOne(x => x.Milestone)
                .HasForeignKey(s => s.MilestoneId);                

            modelBuilder.Entity<Milestone>()
                .ToTable("Milestones")
                .HasMany(r => r.MilestoneAttachments)
                .WithOne(x => x.Milestone)
                .HasForeignKey(s => s.MilestoneId);                

            modelBuilder.Entity<Milestone>()
                .ToTable("Milestones")
                .HasMany(r => r.ContractFunds)
                .WithOne(x => x.Milestone)
                .HasForeignKey(s => s.MilestoneId);

            modelBuilder.Entity<Milestone>()
                .ToTable("Milestones")
                .HasMany(r => r.MilestoneMessages)
                .WithOne(x => x.Milestone)
                .HasForeignKey(s => s.MilestoneId);

            modelBuilder.Entity<MilestoneStatus>()
                .ToTable("MilestoneStatus")
                .HasData(MasterDataFactory.GetMilestoneStatusData());

            modelBuilder.Entity<ContractMilestoneFund>()
                .ToTable("ContractMilestoneFunds");
            modelBuilder.Entity<Card>()
                .ToTable("Cards");

            modelBuilder.Entity<EmailAudit>()
                .ToTable("EmailAudits");
            modelBuilder.Entity<EmailAttachments>()
                .ToTable("EmailAttachments");
            modelBuilder.Entity<AccountDetail>()
                .ToTable("AccountDetails");

            // configures one-to-many relationship
            modelBuilder.Entity<Job>()
                .ToTable("Jobs")
                .HasMany(g => g.JobAttachments)
                .WithOne(s => s.Job)
                .HasForeignKey(s => s.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobBid>()
                .HasMany(g => g.JobBidAttachments)
                .WithOne(x => x.JobBid)
                .HasForeignKey(s => s.JobBidId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobBid>()
                .HasOne(g => g.FreelancerUser)
                .WithMany(s => s.JobBids);

            modelBuilder.Entity<JobBid>()
                .HasOne(g => g.Job)
                .WithMany(s => s.JobBids)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                  .HasMany(g => g.JobBids)
                  .WithOne(s => s.Job)
                  .HasForeignKey(s => s.JobId)
                  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobStatus>()
                .HasData(MasterDataFactory.GetJobStatusData());

            modelBuilder.Entity<JobStatus>()
                .HasMany(g => g.Jobs)
                .WithOne(s => s.JobStatus)
                .HasForeignKey(s => s.JobStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(e => e.MobilePhone)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(g => g.UserPayments)
                .WithOne(s => s.FromUser)
                .HasForeignKey(s => s.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(g => g.UserWithdrawals)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(g => g.Jobs)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
              .HasMany(g => g.ContractChatSessionClients)
              .WithOne(s => s.Client)
              .HasForeignKey(s => s.ClientId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(g => g.ContractChatMessages)
                .WithOne(s => s.SenderUser)
                .HasForeignKey(s => s.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>()
                .HasMany(g => g.Users)
                .WithOne(x => x.Country)
                .HasForeignKey(s => s.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractChatMessage>()
                .HasOne(g => g.ContractChatSession)
                .WithMany(h=>h.ContractChatMessages)
                .HasForeignKey(s => s.ContractChatSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractChatSession>()
                .HasOne(g => g.Freelancer)
                .WithMany(h=>h.ContractChatSessionsFreelancers)
                .HasForeignKey(s => s.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractChatSessionUser>()
               .HasOne(g => g.User)
               .WithMany(h=>h.ContractChatSessionUsers)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
               .HasMany(g => g.ContractChatSessions)
               .WithOne(s => s.Job)
               .HasForeignKey(s => s.JobId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobBidChatSession>()
                .HasOne(g => g.Freelancer)
                .WithMany(h=>h.JobBidChatSessionsFreelancers)
                .HasForeignKey(s => s.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobBidChatSession>()
               .HasOne(g => g.Client)
               .WithMany(h => h.JobBidChatSessionClients)
               .HasForeignKey(s => s.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Milestone>()
                .HasOne(g => g.Contract)
                .WithMany(h => h.Milestones)
                .HasForeignKey(s => s.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Milestone>()
               .HasOne(g => g.CreatedBy)
               .WithMany(h => h.Milestones)
               .HasForeignKey(s => s.CreatedId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Milestone>()
                .HasOne(g => g.CreatedBy)
                .WithMany(h => h.Milestones)
                .HasForeignKey(s => s.CreatedId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MilestoneAttachment>()
                .ToTable("MilestoneAttachments")
                .HasOne(g => g.Milestone);

            modelBuilder.Entity<ContractMilestoneFund>()
                .HasOne(g => g.User)
                .WithMany(h => h.ContractFunds)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContractMilestoneFund>()
               .HasOne(g => g.Milestone)
               .WithMany(h => h.ContractFunds)
               .HasForeignKey(s => s.MilestoneId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Card>()
              .HasOne(g => g.User)
              .WithMany(h => h.Cards)
              .HasForeignKey(s => s.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmailAudit>()
             .HasOne(g => g.User)
             .WithMany(h => h.EmailAudits)
             .HasForeignKey(s => s.ReceiverId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MilestoneRequestPayer>()
             .ToTable("MilestoneRequestPayers")
             .HasOne(g => g.Milestone);

            modelBuilder.Entity<MilestoneRequestPayer>()
            .ToTable("MilestoneRequestPayers")
            .HasOne(g => g.PayerStatus);

            modelBuilder.Entity<UserPayment>()
                        .ToTable("UserPayments")
                        .HasOne(g => g.FromUser);

            modelBuilder.Entity<UserPayment>()
                        .HasOne(g => g.ToUser);

            modelBuilder.Entity<UserPayment>()
                        .HasOne(g => g.FromUser);


            modelBuilder.Entity<UserPayment>()
                .HasOne(g => g.PaymentTransactionType)
                .WithMany(h => h.UserPayments)
                .HasForeignKey(s => s.PaymentTransactionTypeId);

            modelBuilder.Entity<PaymentTransactionType>()
                .ToTable("PaymentTransactionTypes")
                .HasData(MasterDataFactory.GetPaymentTransactionTypeData()
                );

            modelBuilder.Entity<UserBalance>()
                        .ToTable("UserBalances")
                        .HasOne(g => g.User);


            modelBuilder.Entity<UserBalance>()
                .HasOne(g => g.UserPayment)
                .WithMany(h => h.UserBalances)
                .HasForeignKey(s => s.UserPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemBalance>()
                        .ToTable("SystemBalances");

            modelBuilder.Entity<SystemPayment>()
                        .HasIndex(b => b.MilestoneRequestPayerId);

            modelBuilder.Entity<SystemPayment>()
                        .HasIndex(b => b.MilestoneSystemRequestPayerId);

            modelBuilder.Entity<SystemPayment>()
                        .HasIndex(b => b.ToUserId);

            modelBuilder.Entity<SystemPayment>()
                .HasOne(g => g.PaymentTransactionType)
                .WithMany(h => h.SystemPayments)
                .HasForeignKey(s => s.PaymentTransactionTypeId);

            modelBuilder.Entity<UserWithdrawal>()
                .ToTable("UserWithdrawals")
                .HasOne(g => g.User);

            modelBuilder.Entity<UserWithdrawal>()
                .HasOne(g => g.PaymentTransactionType)
                .WithMany(h => h.UserWithdrawals)
                .HasForeignKey(s => s.PaymentTransactionTypeId);

            modelBuilder.Entity<UserWithdrawal>()
                .ToTable("UserWithdrawals")
                .HasOne(g => g.PayerStatus);

            modelBuilder.Entity<FeeStructure>()
                .ToTable("FeeStructures")
                .HasData(MasterDataFactory.GetFeeStructureData());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        private void AddAuditInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (((BaseEntity)entry.Entity).CreatedDate == DateTime.UtcNow)
                        ((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
                }
                //if (((BaseEntity)entry.Entity).ModifiedDate == DateTime.UtcNow)
                    ((BaseEntity)entry.Entity).ModifiedDate = DateTime.UtcNow;
            }
        }
    }
}
