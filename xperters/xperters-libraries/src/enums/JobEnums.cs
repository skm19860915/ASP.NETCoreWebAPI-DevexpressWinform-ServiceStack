using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace xperters.enums
{
    public class JobEnums
    {
        public enum JobType
        {
            [Description("I am not sure")]
            Iamnotsure = 1,
            [Description("One Time project")]
            OneTimeproject = 2,
            [Description("Ongoing project")]
            Ongoingproject = 3
        }

        public enum JobCategory
        {
            [Description("Web, Mobile & Software Dev")]
            WebMobileAndSoftwareDev = 1,
            [Description("IT & Networking")]
            ITAndNetworking = 2,
            [Description("Data Science & Analytics")]
            DataScienceAndAnalytics = 3,
            [Description("Engineering & Architecture")]
            EngineeringAndArchitecture = 4,
            [Description("Design & Creative")]
            DesignAndCreative = 5,
            [Description("Writing")]
            Writing = 6,
            [Description("Translation")]
            Translation = 7,
            [Description("Legal")]
            Legal = 8,
            [Description("Admin Support")]
            AdminSupport = 9,
            [Description("Customer Service")]
            CustomerService = 10,
            [Description("Sales & Marketing")]
            SalesAndMarketing = 11,
            [Description("Display Advertising")]
            DisplayAdvertising = 12,
            [Description("Email & Marketing Automation")]
            EmailAndMarketingAutomation = 13,
            [Description("Lead Generation")]
            LeadGeneration = 14,
            [Description("Market & Customer Research")]
            MarketAndCustomerResearch = 15,
            [Description("Marketing Strategy")]
            MarketingStrategy = 16,
            [Description("Public Relations")]
            PublicRelations = 17,
            [Description("SEM - Search Engine Marketing")]
            SEMSearchEngineMarketing = 18,
            [Description("SEO - Search Engine Optimization")]
            SEOSearchEngineOptimization = 19,
            [Description("Telemarketing & Telesales")]
            TelemarketingAndTelesales = 20,
            [Description("Other - Sales & Marketing")]
            OtherSalesAndMarketing = 21,
            [Description("Accounting & Consulting")]
            AccountingAndConsulting = 22,
        }

        public enum JobBidStatus
        {
            [Description("NoBid")]
            NoBid = 1,
            [Description("BidSubmitted")]
            BidsSubmitted = 2,
            [Description("BidSelected")]
            BidSelected = 3,
            [Description("Bid Amendment")]
            BidAmendment = 4,
            [Description("Bid Updated")]
            BidUpdatedByFreelancer = 5
        }
        public enum JobStatus
        {
            [Description("Job Posted")]
            JobPosted = 1,
            [Description("ContractSigned")]
            ContractSigned = 2,
            [Description("Job InProgress")]
            JobInProgress = 3,
            [Description("Job Completed")]
            JobCompleted = 4,
            [Description("Job Canceled")]
            JobCanceled = 5

        }

        public enum JobDuration
        {
            [Description("Any Project Length")]
            AnyProjectLength = 1,
            [Description("Less than 1 month")]
            LessThanOneMonth = 2,
            [Description("One To Three Months")]
            OneToThreeMonths = 3,
            [Description("Long Term")]
            LongTerm = 4
        }
    }
}
