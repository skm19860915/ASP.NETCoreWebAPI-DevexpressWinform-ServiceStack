using System;

namespace xperters.models
{
    public class MilestoneMessageView : BaseView
    {
        public Guid MilestoneId { get; set; }
        public string Description { get; set; }
        public Guid CreatedId { get; set; }
        public UserView Created { get; set; }

        public string CreatedBy { get; set; }
     }
}
