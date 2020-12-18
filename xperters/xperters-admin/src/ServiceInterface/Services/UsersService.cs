using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using xperters.business.Interfaces;
using Xperters.Admin.ServiceModel;
using Xperters.Admin.ServiceModel.Exceptions;
using Xperters.Admin.ServiceModel.Users;

namespace Xperters.Admin.ServiceInterface.Services
{
    public class UsersService : ServiceBase
    {
        private readonly IUserManager _userManager;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IUserManager userManager, ILogger<UsersService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager;
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole)]
        public GetUserInfoForAdminResponse Get(GetUserInfoForAdminRequest request)
        {
            _logger.LogDebug("Get user information for {@request}", request);

            var userInfos = _userManager.GetUserInfos(request.Page, 100);

            if (userInfos == null)
            {
                throw new XpertersException($"Failed to find data for : {request.Page}");
            }

            return new GetUserInfoForAdminResponse
            {
                UserInfos = userInfos
            };
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole, SecurityConstants.UserRoles.ReadRole)]
        public GetUserInfoForAdminResponse Post(PostParamsForFilteredUserInfoRequest request)
        {
            _logger.LogDebug("Get user information for {@request}", request);

            var list = _userManager.GetFilteredUserInfos(request.Name, request.CreatedDate);

            return new GetUserInfoForAdminResponse
            {
                UserInfos = list
            };
        }
    }
}
