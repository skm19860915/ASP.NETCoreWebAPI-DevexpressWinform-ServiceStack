using System;
using System.Collections.Generic;

namespace xperters.domain
{
    public class UserInfoDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public string UserRole { get; set; }
        public DateTime Created { get; set; }
        public bool IsEnabled { get; set; }
        public string Country { get; set; }
        public List<JobDto> Jobs { get; set; }
    }
}
