using System.Threading.Tasks;
using Xperters.Admin.ServiceModel.Users;

namespace Xperters.Admin.UI.Tabs.ServiceInterfaces
{
    public interface IUserServiceClient
    {
        Task<GetUserInfoForAdminResponse> GetAsync(GetUserInfoForAdminRequest request);
        Task<GetUserInfoForAdminResponse> PostAsync(PostParamsForFilteredUserInfoRequest request);
    }
}
