using System.Threading.Tasks;
using xperters.azuread.Responses;

namespace xperters.azuread.Interfaces
{
    public interface IMobileNumberService
    {
        Task<string> GetUserMobileNumber();
        Task<UserDetailResponse> GetUserDetail();
    }
}