using System;
using System.Collections.Generic;
using xperters.enums;

namespace xperters.domain
{
    public class UserWithdrawalDto : BaseReadOnlyDto
    {
        public Guid UserId { get; set; }            

        public decimal Amount { get; set; }
        public decimal BalanceOld { get; set; }
        public decimal BalanceNew { get; set; }
        public int CurrencyId { get; set; }
        public int PayerStatusId { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime? LastPaymentServiceStatusCheck { get; set; }
        public int? PaymentServiceCheckCount { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }            // whether this is a credit or debit transaction
        public PaymentTransactionTypeDto PaymentTransactionType { get; set; }
        public RequestPayerStatusDto PayerStatus { get; set; }
        public UserDto User { get; set; }        
        
    }
}
