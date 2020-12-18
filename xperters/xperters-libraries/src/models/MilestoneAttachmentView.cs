using System;

namespace xperters.models
{
    public class MilestoneAttachmentView : AttachmentView
    {
        public Guid  MilestoneId {get;set;}
        public MilestoneView Milestone { get; set; }
    }
}
