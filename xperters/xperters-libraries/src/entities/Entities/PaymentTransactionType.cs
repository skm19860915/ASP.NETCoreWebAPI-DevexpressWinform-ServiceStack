using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using xperters.enums;

namespace xperters.entities.Entities
{
    public class PaymentTransactionType
    {
        [Key]
        public Enums.PaymentTransactionType PaymentTransactionTypeId { get; set; }

        [Required]
        public string Type { get; set; }
        public virtual ICollection<UserPayment> UserPayments { get; set; }
        public virtual ICollection<UserWithdrawal> UserWithdrawals { get; set; }
        public virtual ICollection<SystemPayment> SystemPayments { get; set; }
    }
}
