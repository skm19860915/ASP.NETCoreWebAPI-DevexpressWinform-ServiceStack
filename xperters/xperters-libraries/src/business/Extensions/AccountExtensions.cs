using System.Security.Claims;
using xperters.constants;

namespace xperters.business.Extensions
{
    public static class AccountExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimsConstants.UserIdentifier)?.Value;
        }      
    }
}
