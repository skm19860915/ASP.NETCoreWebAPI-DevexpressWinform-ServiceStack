using System;
using System.Collections.Generic;
using System.Text;

namespace xperters.models
{
    public class UserPaymentView
    {
        public Guid FromUserId { get; set; }            // This needs to be an actual user Id.  Never System.  System should only appear in the systempayments table
        public Guid ToUserId { get; set; }              // This needs to be an actual user Id.  Never System

        public Guid MilestoneRequestPayerId { get; set; }
        public Guid MilestoneSystemRequestPayerId { get; set; }
        public string PaymentTransactionTypeId { get; set; }
        public int CurrencyId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public decimal Balance { get; set; }

        public string JobDesc { get; set; }

        public string User { get; set; }

        public Guid PaymentId { get; set; }
    }
}
