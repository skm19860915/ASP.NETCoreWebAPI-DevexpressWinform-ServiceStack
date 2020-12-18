using System.Collections.Generic;
using ServiceStack;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Milestones
{
    public sealed class GetMilestoneSystemRequestPayersResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<MilestoneSystemRequestPayerDto> MilestoneSystemRequestPayers { get; set; }
    }
}