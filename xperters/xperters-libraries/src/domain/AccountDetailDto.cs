using System;


namespace xperters.domain
{
   public class AccountDetailDto:BaseDto
    {

        public Guid UserId { get; set; }

        public UserDto User { get; set; }

        public string AccountHolderName { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string IfscCode { get; set; }

        public string SwiftNumber { get; set; }

        public string BankAddress { get; set; }

    }
}
