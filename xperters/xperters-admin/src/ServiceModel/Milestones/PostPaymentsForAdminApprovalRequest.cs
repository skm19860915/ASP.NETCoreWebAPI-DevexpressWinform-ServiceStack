using System.Collections.Generic;
using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;
using System;

namespace Xperters.Admin.ServiceModel.Milestones
{
    [Tag(Constants.Constants.Milestones)]
    [Route("/milestones/payments/approve", RequestTypeConstants.Post)]
    public class PostPaymentsForAdminApprovalRequest : IRequest<PostPaymentsForAdminApprovalResponse>
    {
        public List<Guid> MilestoneIdsToApprove{ get; set; }
        public int Version { get; set; }
    }
}
