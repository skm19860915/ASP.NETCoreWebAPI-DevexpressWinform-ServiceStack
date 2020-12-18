using System;
using System.Collections.Generic;
using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Jobs
{
    [Tag(Constants.Constants.Jobs)]
    [Route("/job/{id}/milestones", RequestTypeConstants.Get)]
    public class GetJobMilestonesRequest : IReturn<IEnumerable<MilestoneDto>>
    {
        public Guid Id { get; set; }
    }
}