using System;

namespace xperters.domain
{
    public class BaseReadOnlyDto
    {
        public BaseReadOnlyDto()
        {
            CreatedDate = DateTime.UtcNow;
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}