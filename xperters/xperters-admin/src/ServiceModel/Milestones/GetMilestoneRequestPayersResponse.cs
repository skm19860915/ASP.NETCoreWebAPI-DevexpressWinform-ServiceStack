using System.Collections.Generic;
using ServiceStack;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Milestones
{
    public sealed class GetMilestoneRequestPayersResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<MilestoneRequestPayerDto> MilestoneRequestPayers { get; set; }
    }
}