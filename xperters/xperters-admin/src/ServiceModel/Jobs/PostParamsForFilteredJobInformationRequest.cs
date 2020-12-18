using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Jobs
{
    [Tag(Constants.Constants.Jobs)]
    [Route("/jobs/information/filter", RequestTypeConstants.Post)]
    public class PostParamsForFilteredJobInformationRequest : IRequest<GetJobInformationForAdminResponse>
    {
        public string JobTitle { get; set; }
        public string CreatedDate { get; set; }
        public int Version { get; set; }
    }
}
