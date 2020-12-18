using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Milestones
{
    [Tag(Constants.Constants.Milestones)]
    [Route("/milestones/payments/{page}/{numberperpage}", RequestTypeConstants.Get)]
    public class GetPaymentsForAdminApprovalRequest : IRequest<GetPaymentsForAdminApprovalResponse>
    {
        public int Page { get; set; }
        public int NumberPerPage { get; set; }
        public int Version { get; set; }
    }
}
