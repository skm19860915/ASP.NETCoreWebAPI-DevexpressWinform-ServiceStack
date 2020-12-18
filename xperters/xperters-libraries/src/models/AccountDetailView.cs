using System;

namespace xperters.models
{
   public class AccountDetailView:BaseView
    {

        public Guid UserId { get; set; }

        public UserView User { get; set; }

        public string AccountHolderName { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string IfscCode { get; set; }

        public string SwiftNumber { get; set; }

        public string BankAddress { get; set; }
    }
}
