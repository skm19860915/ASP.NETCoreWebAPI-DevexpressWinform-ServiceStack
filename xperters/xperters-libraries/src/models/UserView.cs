using System;

namespace xperters.models
{
    public class UserView : BaseView
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public int CountryId { get; set; }

        public bool IsActive { get; set; }       
        public int UserRole { get; set; }
        public bool IsEnabled { get; set; }
    }
}
