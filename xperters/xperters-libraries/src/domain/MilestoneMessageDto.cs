using System;

namespace xperters.domain
{
    public class MilestoneMessageDto:BaseDto
    {
        public Guid MilestoneId { get; set; }
        public string Description { get; set; }
        public Guid CreatedId { get; set; }
        public UserDto Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
