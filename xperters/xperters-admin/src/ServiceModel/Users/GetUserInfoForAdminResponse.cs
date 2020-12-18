using ServiceStack;
using System.Collections.Generic;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Users
{
    public class GetUserInfoForAdminResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<UserInfoDto> UserInfos { get; set; }
    }
}
