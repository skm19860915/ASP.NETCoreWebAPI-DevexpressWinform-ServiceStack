using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Users
{
    [Tag(Constants.Constants.Jobs)]
    [Route("/users/information/filter", RequestTypeConstants.Post)]
    public class PostParamsForFilteredUserInfoRequest : IRequest<GetUserInfoForAdminResponse>
    {
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public int Version { get; set; }
    }
}
