using System;
using System.Collections.Generic;

namespace Xperters.Admin.UI.Tabs.UserTab
{
    public class UserInfoViewModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public string UserRole { get; set; }
        public DateTime Created { get; set; }
        public bool IsEnabled { get; set; }
        public string Country { get; set; }
        public int JobsCreated { get; set; }
        public int JobsWorkedOn { get; set; }
        public List<JobDetailViewModel> Jobs { get; set; }
    }

    public class JobDetailViewModel
    {
        public string JobTitle { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
