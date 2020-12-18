using ServiceStack;
using System;
using System.Collections.Generic;

namespace Xperters.Admin.ServiceModel.Milestones
{
    public sealed class PostPaymentsForAdminApprovalResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public List<Guid> UpdatedMilestoneIds { get; set; }
    }
}