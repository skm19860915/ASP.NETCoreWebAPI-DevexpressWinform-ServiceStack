using System;
using System.Collections.Generic;
using System.Linq;
using xperters.entities.Entities;

namespace xperters.entities
{
    public static class MasterDataFactory
    {
        public static IEnumerable<Category> GetCategoryData()
        {
            var list = new List<Category>
            {
                new Category {Id = 1, CategoryName = "Web, Mobile & Software Dev", IsActive = true },
                new Category {Id = 2, CategoryName = "IT & Networking", IsActive = true },
                new Category {Id = 3, CategoryName = "Data Science & Analytics", IsActive = true },
                new Category {Id = 4, CategoryName = "Engineering & Architecture", IsActive = true },
                new Category {Id = 5, CategoryName = "Design & Creative", IsActive = true },
                new Category {Id = 6, CategoryName = "Writing", IsActive = true },
                new Category {Id = 7, CategoryName = "Translation", IsActive = true },
                new Category {Id = 8, CategoryName = "Legal", IsActive = true },
                new Category {Id = 9, CategoryName = "Admin Support", IsActive = true },
                new Category {Id = 10, CategoryName = "Customer Service", IsActive = true },
                new Category {Id = 11, CategoryName = "Sales & Marketing", IsActive = true },
                new Category {Id = 12, CategoryName = "Display Advertising", IsActive = true },
                new Category {Id = 13, CategoryName = "Email & Marketing Automation", IsActive = true },
                new Category {Id = 14, CategoryName = "Lead Generation", IsActive = true },
                new Category {Id = 15, CategoryName = "Market & Customer Research", IsActive = true },
                new Category {Id = 16, CategoryName = "Marketing Strategy", IsActive = true },
                new Category {Id = 17, CategoryName = "Public Relations", IsActive = true },
                new Category {Id = 18, CategoryName = "SEM - Search Engine Marketing", IsActive = true },
                new Category {Id = 19, CategoryName = "SEO - Search Engine Optimization", IsActive = true },
                new Category {Id = 20, CategoryName = "Telemarketing & Telesales", IsActive = true },
                new Category {Id = 21, CategoryName = "Other - Sales & Marketing", IsActive = true },
                new Category {Id = 22, CategoryName = "Accounting & Consulting", IsActive = true }
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<Skill> GetSkillData()
        {
            var list = new List<Skill>
            {
                new Skill {Id = 1, SkillName =  "C++", IsActive = true },
                new Skill {Id = 2, SkillName =  "C#", IsActive = true },
                new Skill {Id = 3, SkillName =  "OOPS", IsActive = true },
                new Skill {Id = 4, SkillName =  "AngularJs", IsActive = true },
                new Skill {Id = 5, SkillName =  "Javascript", IsActive = true },
                new Skill {Id = 6, SkillName =  "JQuery", IsActive = true },
                new Skill {Id = 7, SkillName =  "MongoDB", IsActive = true },
                new Skill {Id = 8, SkillName =  "SQL Server", IsActive = true }
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<Country> GetCountryData()
        {
            var list = new List<Country>
            {
                new Country {Id = 256, CountryName =  "Uganda", CountryCode = "UG", IsActive = true },
                new Country {Id = 255, CountryName =  "Tanzania", CountryCode = "TZ", IsActive = true },
                new Country {Id = 254, CountryName =  "Kenya", CountryCode = "KE", IsActive = true },
                new Country {Id = 257, CountryName =  "Burundi", CountryCode = "BI", IsActive = true },
                new Country {Id = 249, CountryName =  "Sudan", CountryCode = "SD", IsActive = true },
                new Country {Id = 211, CountryName =  "South Sudan", CountryCode = "SS", IsActive = true },
                new Country {Id = 252, CountryName =  "Somalia", CountryCode = "SO", IsActive = true },
                new Country {Id = 250, CountryName =  "Rwanda", CountryCode = "RW", IsActive = true },
                new Country {Id = 971, CountryName =  "United Arab Emirates", CountryCode = "AE", IsActive = true },
                new Country {Id = 1, CountryName =  "United States", CountryCode = "US", IsActive = true },
                new Country {Id = 44, CountryName =  "United Kingdom", CountryCode = "GB", IsActive = true },
                new Country {Id = 91, CountryName =  "India", CountryCode = "IN", IsActive = true },
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<Currency> GetCurrenciesData()
        {
            var list = new List<Currency>
            {
                new Currency {CurrencyId = 1, CurrencyCode =  "USD"},
                new Currency {CurrencyId = 2, CurrencyCode =  "EUR"},
            };

            return list.AsEnumerable();
        }
        public static IEnumerable<JobStatus> GetJobStatusData()
        {
            var list = new List<JobStatus>
            {
                new JobStatus {JobStatusId = 1, Status =  "Job Posted", IsActive = true },
                new JobStatus {JobStatusId = 2, Status =  "ContractSigned", IsActive = true },
                new JobStatus {JobStatusId = 3, Status =  "Job In-Progress", IsActive = true },
                new JobStatus {JobStatusId = 4, Status =  "Job Completed", IsActive = true },
                new JobStatus {JobStatusId = 5, Status =  "Job Canceled", IsActive = true },
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<RequestPayerStatus> GetRequestPayerStatusData()
        {
            var list = new List<RequestPayerStatus>
            {
                new RequestPayerStatus {PayerStatusId = 1, PayerStatus =  "Successful", IsActive = true },
                new RequestPayerStatus {PayerStatusId = 2, PayerStatus =  "Pending", IsActive = true },
                new RequestPayerStatus {PayerStatusId = 3, PayerStatus =  "Cancelled", IsActive = true },
                new RequestPayerStatus {PayerStatusId = 4, PayerStatus =  "Failed", IsActive = true },
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<MilestoneStatus> GetMilestoneStatusData()
        {
            var list = new List<MilestoneStatus>
            {
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.AddFunds, StatusDescription =  "Add Funds", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.Active, StatusDescription =  "Active", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.MilestoneCompletedWaitingForClientReview, StatusDescription =  "Milestone completed. Waiting For client approval", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.AdminApprovedFundsPaidToFreelancerWallet, StatusDescription =  "Admin approved. Milestone funds paid to freelancer wallet", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.PaidAfterFreelancerWithdrawal, StatusDescription =  "Paid after freelancer withdrawal", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.ClientCancelledPendingRefund, StatusDescription = "Milestone cancelled by client. Pending refund", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.PaymentRejected, StatusDescription =  "Payment rejected", IsActive = true }, 
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.ClientApproved, StatusDescription =  "Client approved. Awaiting admin review", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.FreelancerWithdrawal, StatusDescription =  "Freelancer withdrawal", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.FreelancerCancelledPendingRefund, StatusDescription =  "Milestone cancelled by freelancer. Pending refund", IsActive = true },
                new MilestoneStatus {MilestoneStatusId = (int)enums.Enums.MilestoneStatus.ClientRefunded, StatusDescription =  "Client refunded", IsActive = true }
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<PaymentTransactionType> GetPaymentTransactionTypeData()
        {
            var list = new List<PaymentTransactionType>
            {
                new PaymentTransactionType {PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Credit, Type = "Credit"},
                new PaymentTransactionType {PaymentTransactionTypeId = enums.Enums.PaymentTransactionType.Debit, Type = "Debit" }
            };

            return list.AsEnumerable();
        }

        public static IEnumerable<FeeStructure> GetFeeStructureData()
        {
            var list = new List<FeeStructure>
            {
                new FeeStructure {Id = new Guid("{11111111-0000-0000-0000-000000000001}"), Description = "0-500", BandStart = 0, BandEnd = 500, FeeFlatRate = 20m, FeePercentage = 5m},
                new FeeStructure {Id = new Guid("{11111111-0000-0000-0000-000000000002}"), Description = "500-1000", BandStart = 500, BandEnd = 1000, FeeFlatRate = 15m, FeePercentage = 5m },
                new FeeStructure {Id = new Guid("{11111111-0000-0000-0000-000000000003}"), Description = "1000-10000", BandStart = 1000, BandEnd = 10000, FeeFlatRate = 10m, FeePercentage = 4m },
                new FeeStructure {Id = new Guid("{11111111-0000-0000-0000-000000000004}"), Description = "10000-100000", BandStart = 10000, BandEnd = 100000, FeeFlatRate = 10m, FeePercentage = 3.5m},
                new FeeStructure {Id = new Guid("{11111111-0000-0000-0000-000000000005}"), Description = ">100000", BandStart = 100000, BandEnd = 0, FeeFlatRate = 10m, FeePercentage = 3m }
            };

            return list.AsEnumerable();
        }        
    }
}
