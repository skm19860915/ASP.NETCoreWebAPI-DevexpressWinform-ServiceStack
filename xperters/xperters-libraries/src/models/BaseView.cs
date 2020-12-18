using System;

namespace xperters.models
{
    public class BaseView
    {
        public BaseView()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = CreatedDate;
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
