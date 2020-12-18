using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Users;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.UserTab.ServiceClient
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly IXpertersAdminServiceClient _serviceClient;
        public UserServiceClient(IXpertersAdminServiceClient serviceClient)
        {
            _serviceClient = serviceClient ?? throw new System.ArgumentNullException(nameof(serviceClient));
        }
        public async Task<GetUserInfoForAdminResponse> GetAsync(GetUserInfoForAdminRequest request)
        {
            return await _serviceClient.GetAsync(request);
        }

        public async Task<GetUserInfoForAdminResponse> PostAsync(PostParamsForFilteredUserInfoRequest request)
        {
            return await _serviceClient.PostAsync(request);
        }
    }
}
