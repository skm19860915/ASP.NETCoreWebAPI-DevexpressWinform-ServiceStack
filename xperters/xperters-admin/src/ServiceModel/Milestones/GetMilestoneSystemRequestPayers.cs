using System;
using System.Collections.Generic;
using ServiceStack;
using xperters.domain;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Milestones
{
    [Tag(Constants.Constants.Milestones)]
    [Route("/milestones/{id}/systemrequestpayers", RequestTypeConstants.Get)]
    public class GetMilestoneSystemRequestPayers : IReturn<IEnumerable<MilestoneSystemRequestPayerDto>>
    {
        public Guid Id { get; set; }
    }
}