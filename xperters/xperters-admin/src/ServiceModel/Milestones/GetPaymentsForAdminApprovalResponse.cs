using System.Collections.Generic;
using ServiceStack;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Milestones
{
    public sealed class GetPaymentsForAdminApprovalResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<MilestonePaymentDto> MilestonePaymentsForAdminApproval{ get; set; }
    }
}