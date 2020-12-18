using System.Collections.Generic;

namespace xperters.domain
{
    public class JobStatusDto
    {
        public int JobStatusId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public  ICollection<JobDto> Jobs { get; set; }
    }
}
