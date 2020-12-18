using System.ComponentModel;

namespace xperters.enums
{
    public static class Enums
    {
        public enum EnumtoUse
        {
            [Description("This enum is the dummy you can change its value and description as well or remove this all")]
            Pleasechangemyvalue,
            IamdummyEnum
        }
        public enum ProjectType
        {
            Iamnotsure = 1,
            OneTimeproject = 2,
            Ongoingproject = 3,
        }
        public enum VisibilityType
        {
            Public = 1,
            Private = 2
        }
        public enum PaymentType
        {
            [Description("PayPal")]
            PayPal = 1,
            [Description("MPesa")]
            MPesa = 2,
            [Description("Payoneer")]
            Payoneer = 3,
            [Description("WireTransfer")]
            WireTransfer = 4,
            [Description("MTN")]
            // ReSharper disable once InconsistentNaming
            MTN = 5
        }

        public enum EstimatedBudget
        {
            [Description("$10-$100")]
            Amount1 = 1,
            [Description("$101-$1000")]
            Amount2 = 2,
            [Description("$1001-$10,000")]
            Amount3 = 3,
            [Description("$10001-$50,000")]
            Amount4 = 4,
            [Description("$50,001-$100,000")]
            Amount5 = 5,
            [Description("$100,000+")]
            Amount6 = 6
        }
        public enum FreelancerExperience
        {
            [Description("AnyLevel")]
            AnyLevel = 1,
            [Description("Entry")]
            Entry = 2,
            [Description("Intermediate")]
            Intermediate = 3,
            [Description("Expert")]
            Expert = 4
        }

        public enum FreelancerType
        {
            [Description("Any Hours Per Week")]
            AnyHoursPerWeek = 1,
            [Description("Part Time")]
            PartTime = 2,
            [Description("Full Time")]
            FullTime = 3
        }
        public enum ClientHistory
        {
            [Description("Any client history")]
            Iamnotsure = 1,
            [Description("No hires")]
            OneTimeproject = 2,
            [Description("1 to 9 hires")]
            Ongoingproject = 3,
            [Description("10+ hires")]
            MoreThan10 = 4
        }

        public enum FileFor
        {
            [Description("Users")]
            Users = 1,
            [Description("AgencyFiles")]
            FreelancerFiles = 2,
            [Description("JobAttachments")]
            JobAttachments = 3,
            [Description("JobBidAttachments")]
            JobBidAttachments = 4,
            [Description("MilestoneAttachments")]
            MilestoneAttachments = 5

        }

        public enum MessageType
        {
            [Description("Completed")]
            Completed = 3,
            [Description("Hired")]
            Hired = 2,
            [Description("Negotiate")]
            Negotiate = 1

        }


        public enum ContractStatus
        {
            [Description("Contract Start")]
            ContractStart = 1,
            [Description("Contract InProgress")]
            ContractInProgress = 2,
            [Description("Contract Completed")]
            ContractCompleted = 3,
        }

        public enum MilestoneStatus
        {
            [ProcessOrder(1)]
            [Description("Add Funds")]
            [FreelancerDescription("Not Funded")]
            AddFunds = 1,

            [ProcessOrder(2)]
            [Description("Active")]
            [FreelancerDescription("Milestone completed")]
            Active = 2,

            [ProcessOrder(3)]
            [Description("Milestone completed. Waiting For client approval")]
            [FreelancerDescription("Awaiting client review")]
            MilestoneCompletedWaitingForClientReview = 3,

            [ProcessOrder(5)]
            [Description("Admin approved. Milestone funds paid to freelancer")]
            [FreelancerDescription("Milestone funds available for withdrawal")]
            AdminApprovedFundsPaidToFreelancerWallet = 4,

            [ProcessOrder(7)]
            [Description("Paid. Transfer successful")]
            [FreelancerDescription("Paid")]
            PaidAfterFreelancerWithdrawal = 5,

            [ProcessOrder(3)]
            [Description("Milestone cancelled by client. Pending refund")]
            [FreelancerDescription("Client cancelled")]
            ClientCancelledPendingRefund = 6,

            [ProcessOrder(2)]
            [Description("Payment rejected by provider. Try again later")]
            [FreelancerDescription("Not Funded")]
            PaymentRejected = 7,

            [ProcessOrder(4)]
            [Description("Client approved. Awaiting admin review")]
            [FreelancerDescription("Client Approved. Awaiting admin review")]            
            ClientApproved = 8,

            [ProcessOrder(6)]  // required for the payment service to know that the withdrawal payment can be made now.
            [Description("Paid, pending transfer")]
            [FreelancerDescription("Freelancer withdrawal")]            
            FreelancerWithdrawal = 9,

            [ProcessOrder(3)]
            [Description("Milestone cancelled by freelancer. Pending refund")]
            [FreelancerDescription("FreelancerCancelledPendingRefund")]            
            FreelancerCancelledPendingRefund = 10,

            [ProcessOrder(4)]
            [Description("ClientRefunded")]
            [FreelancerDescription("ClientRefunded")]            
            ClientRefunded = 11
        }

        public enum UserRole:byte
        {
            [Description("Admin")]
            Admin = 1,
            [Description("Client")]
            Client = 2,
            [Description("Freelancer")]
            Freelancer = 3
         }

        public enum FreeLancerMilestoneStatus
        {
            [Description("Not Funded")]
            NotFunded = 1,
            [Description("Complete")]
            Complete = 2,
            [Description("UnderReview")]
            UnderReview = 3,
            [Description("Approved")]
            Approved = 4,
            [Description("Cancel")]//Paid
            Cancel = 5,
            [Description("Closed")]//Cancel
            Close = 6,
            [Description("Funded")]
            Funded = 7
        }

        public enum FundStatus
        {
            [Description("Payment Completed")]
            Completed = 1,
            [Description("Payment Cancel")]
            Cancel = 2,
            [Description("Payment Pending")]
            Pending = 3,
            [Description("Payment Complete")]
            Complete = 4,
        }

        public enum SenderType
        {
            [Description("Client")]
            Client = 1,
            [Description("Freelancer")]
            Freelancer = 2,
            [Description("Admin")]
            Admin = 3,
        }

        public enum PaymentTransactionType
        {
            [Description("Credit")]
            Credit = 1,
            [Description("Debit")]
            Debit,
        }        
        
        public enum RequestPayerStatus
        {
            [Description("Successful")]
            Successful = 1,
            [Description("Pending")]
            Pending,
            [Description("Cancelled")]
            Cancelled,
            [Description("Failed")]
            Failed,
        }

        public enum CurrencyType
        {
            [Description("USD")]
            USD = 1,
            [Description("EUR")]
            EUR,
        }
    }
}
