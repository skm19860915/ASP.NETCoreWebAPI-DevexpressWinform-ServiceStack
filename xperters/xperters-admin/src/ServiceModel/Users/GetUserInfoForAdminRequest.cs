using ServiceStack;
using Xperters.Admin.ServiceModel.Constants;

namespace Xperters.Admin.ServiceModel.Users
{
    [Tag(Constants.Constants.Users)]
    [Route("/users/info/{page}", RequestTypeConstants.Get)]
    public class GetUserInfoForAdminRequest : IRequest<GetUserInfoForAdminResponse>
    {
        public int Page { get; set; }
        public int Version { get; set; }
    }
}
