using System;
using xperters.domain;
using xperters.enums;

namespace xperters.business.Interfaces
{
   public interface IAccountManager
    {
        UserDto GetUserById(string userId);
        UserDto GetUserByEmail(string email);
        ResultModel Login(UserDto userModel);
        UserDto GetSignedInUser();
        void CreateUser(UserDto user, string countryCode);
        ResultModel UpdateUserRole(Enums.UserRole role);
        void CheckAndUpdateUserCountry(UserDto user, string countryCode);
    }
}
