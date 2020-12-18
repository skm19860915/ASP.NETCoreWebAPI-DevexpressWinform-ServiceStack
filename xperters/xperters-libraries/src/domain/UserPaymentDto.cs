using System;
using xperters.enums;

namespace xperters.domain
{
    public class UserPaymentDto : BaseReadOnlyDto
    {
        public Guid FromUserId { get; set; }            // This needs to be an actual user Id.  Never System.  System should only appear in the systempayments table
        public Guid ToUserId { get; set; }              // This needs to be an actual user Id.  Never System

        public Guid MilestoneRequestPayerId { get; set; }
        public Guid MilestoneSystemRequestPayerId { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }    
        public int CurrencyId { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }            

    }
}
