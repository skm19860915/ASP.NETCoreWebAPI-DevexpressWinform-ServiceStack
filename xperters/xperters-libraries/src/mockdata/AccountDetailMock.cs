using System;
using System.Collections.Generic;
using xperters.domain;

namespace xperters.mockdata
{
    public static class AccountDetailMock
    {
        public static Guid AccountDetailId1;
        public static Guid AccountDetailId2;
        public static Guid AccountDetailId3;
        public static Guid AccountDetailId4;
        public static Guid AccountDetailId5;

        private static readonly List<AccountDetailDto> AccountDetailList;

        public static AccountDetailDto AccountDetail1 => Get()[0];
        public static AccountDetailDto AccountDetail2 => Get()[1];
        public static AccountDetailDto AccountDetail3 => Get()[2];
        public static AccountDetailDto AccountDetail4 => Get()[3];
        public static AccountDetailDto AccountDetail5 => Get()[4];


        static AccountDetailMock()
        {
            AccountDetailId1 = Guid.Parse("{90000000-0000-0000-0000-000000000001}");
            AccountDetailId2 = Guid.Parse("{90000000-0000-0000-0000-000000000002}");
            AccountDetailId3 = Guid.Parse("{90000000-0000-0000-0000-000000000003}");
            AccountDetailId4 = Guid.Parse("{90000000-0000-0000-0000-000000000004}");
            AccountDetailId5 = Guid.Parse("{90000000-0000-0000-0000-000000000005}");
            AccountDetailList = new List<AccountDetailDto>
            {
                new AccountDetailDto
                {
                    Id=AccountDetailId1,
                    AccountHolderName="abc",
                    BankAccountNumber="12345",
                    BankName="sbi",
                    BranchName="patiala",
                    IfscCode="sbi123",
                    SwiftNumber="sbi123ss",
                    BankAddress="# 5454, mohali",
                },
                 new AccountDetailDto
                {
                    Id=AccountDetailId2,
                    AccountHolderName="abc",
                    BankAccountNumber="12345",
                    BankName="sbi",
                    BranchName="patiala",
                    IfscCode="sbi123",
                    SwiftNumber="sbi123ss",
                    BankAddress="# 5454, mohali",
                },
                   new AccountDetailDto
                {
                    Id=AccountDetailId3,
                    AccountHolderName="abc",
                    BankAccountNumber="12345",
                    BankName="sbi",
                    BranchName="patiala",
                    IfscCode="sbi123",
                    SwiftNumber="sbi123ss",
                    BankAddress="# 5454, mohali",
                },
                     new AccountDetailDto
                {
                    Id=AccountDetailId4,
                    AccountHolderName="abc",
                    BankAccountNumber="12345",
                    BankName="sbi",
                    BranchName="patiala",
                    IfscCode="sbi123",
                    SwiftNumber="sbi123ss",
                    BankAddress="# 5454, mohali",
                },
                       new AccountDetailDto
                {
                    Id=AccountDetailId5,
                    AccountHolderName="abc",
                    BankAccountNumber="12345",
                    BankName="sbi",
                    BranchName="patiala",
                    IfscCode="sbi123",
                    SwiftNumber="sbi123ss",
                    BankAddress="# 5454, mohali",
                },
            };
        }
        public static List<AccountDetailDto> Get()
        {
            return AccountDetailList;
        }

    }
}
