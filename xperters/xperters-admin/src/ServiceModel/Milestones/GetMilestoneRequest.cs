using System;
using ServiceStack;
using xperters.domain;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Milestones
{
    [Tag(Constants.Constants.Milestones)]
    [Route("/milestones/{Id}", RequestTypeConstants.Get)]
    public class GetMilestoneRequest : IReturn<MilestoneDto>
    {
        public Guid Id { get; set; }
    }
}
