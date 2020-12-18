using ServiceStack;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Milestones
{
    public sealed class GetMilestoneResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public MilestoneDto Milestone { get; set; }
    } 
}