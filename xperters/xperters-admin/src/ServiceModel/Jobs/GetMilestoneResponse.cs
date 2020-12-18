using System.Collections.Generic;
using ServiceStack;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Jobs
{
    public sealed class GetMilestonesResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<MilestoneDto> Milestones { get; set; }
    }
}