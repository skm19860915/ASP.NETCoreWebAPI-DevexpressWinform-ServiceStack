using System;
using System.Collections.Generic;
using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Milestones
{
    [Tag(Constants.Constants.Milestones)]
    [Route("/milestones/{id}/requestpayers", RequestTypeConstants.Get)]
    public class GetMilestoneRequestPayers : IReturn<IEnumerable<MilestoneRequestPayerDto>>
    {
        public Guid Id { get; set; }
    }
}