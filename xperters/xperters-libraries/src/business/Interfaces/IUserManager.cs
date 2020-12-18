using System.Collections.Generic;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IUserManager
    {
        IEnumerable<UserInfoDto> GetUserInfos(int page, int pageSize);
        IEnumerable<UserInfoDto> GetFilteredUserInfos(string name, string createdDate);
    }
}
