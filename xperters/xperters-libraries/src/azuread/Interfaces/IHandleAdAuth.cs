using System.Threading.Tasks;
using xperters.azuread.Requests;
using xperters.azuread.Responses;
using xperters.enums;

namespace xperters.azuread.Interfaces
{
    public interface IHandleAdAuth
    {
        void AddTokenToRequestHeaders(string token);
        Task<UserDetailResponse> GetAadUserDetails(string userId, string graphEndpoint);
        Task UpdateAadUserDetails(string userId, UserDetailsRequest userDetails, string graphEndpoint);
        Task<AuthTokenResponse> GetTokenForGraphAccess(string grantType);
        Task<string> GetAccessToken();
        Task<Enums.UserRole> UpdateUserRole(string userId, Enums.UserRole role);
        Task<string> GetUserMobileNumberFromAuthContact(string userId);
    }
}