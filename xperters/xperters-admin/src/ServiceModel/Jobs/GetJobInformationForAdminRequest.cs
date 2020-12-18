using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Jobs
{
    [Tag(Constants.Constants.Jobs)]
    [Route("/jobs/information/{page}", RequestTypeConstants.Get)]
    public class GetJobInformationForAdminRequest : IRequest<GetJobInformationForAdminResponse>
    {
        public int Page { get; set; }
        public int Version { get; set; }
    }
}
