using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace xperters.entities.Entities
{
    public class AccountDetail : BaseEntity
    {

        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string AccountHolderName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BankAccountNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BankName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string BranchName { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string IfscCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string SwiftNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string BankAddress { get; set; }

    }
}
