using System;
using xperters.enums;

namespace xperters.domain
{
    public class SystemPaymentDto : BaseReadOnlyDto
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public Guid MilestoneRequestPayerId { get; set; }
        public Guid MilestoneSystemRequestPayerId { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }            // whether this is a credit or debit transaction
        public int CurrencyId { get; set; }

        public decimal Amount { get; set; }
        public decimal FeeFlat { get; set; }
        public decimal FeePercent { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal Balance { get; set; }            // system balance

    }
}
