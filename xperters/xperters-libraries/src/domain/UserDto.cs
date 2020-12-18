using System.Collections.Generic;
using static xperters.enums.Enums;

namespace xperters.domain
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        
        public string Address { get; set; } 
        public string MobilePhone { get; set; }
        public bool IsActive { get; set; }        
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public CountryDto Country { get; set; }
        public  UserRole  UserRole { get; set; }
        public bool IsEnabled { get; set; }
        public List<JobDto> Jobs { get; set; }
        public List<JobBidDto> JobBids { get; set; }

        public List<UserPaymentDto> UserPayments { get; set; }
        public List<UserBalanceDto> UserBalances { get; set; }
        public List<UserWithdrawalDto> UserWithdrawals { get; set; }
    }
}
