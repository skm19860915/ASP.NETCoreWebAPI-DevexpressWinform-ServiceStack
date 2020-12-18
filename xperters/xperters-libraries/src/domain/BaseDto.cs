using System;

namespace xperters.domain
{
    public class BaseDto : BaseReadOnlyDto
    {
        protected BaseDto()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = CreatedDate;
        }
        public DateTime ModifiedDate { get; set; }
    }
}
