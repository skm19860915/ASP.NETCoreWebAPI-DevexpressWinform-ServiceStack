using System;
using xperters.enums;

namespace xperters.domain
{
    public class UserPaymentHistoryDto
    {

        public Guid PaymentId { get; set; }
 
        public string DisplayName { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
        public Guid MilestoneId { get; set; }
        public string MilestoneDescription { get; set; }

        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }
        public string JobDescription { get; set; }

    }

}
