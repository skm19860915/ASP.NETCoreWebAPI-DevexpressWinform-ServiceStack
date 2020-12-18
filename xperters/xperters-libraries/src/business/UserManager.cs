using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using xperters.business.Interfaces;
using xperters.domain;
using xperters.repositories;

namespace xperters.business
{
    public class UserManager : IUserManager
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IRepositoryReadOnly<UserInfoDto> _userInfoRepository;
        private ILogger<UserManager> _logger;

        public UserManager(ILoggerFactory loggerFactory, IRepositoryReadOnly<UserInfoDto> userInfoRepository)
        {
            _loggerFactory = loggerFactory;
            _userInfoRepository = userInfoRepository;
            _logger = _loggerFactory.CreateLogger<UserManager>();
        }

        public IEnumerable<UserInfoDto> GetUserInfos(int page, int pageSize)
        {
            var userInfos = _userInfoRepository.Get(page, pageSize);
            _logger.LogDebug("Retrieved user admin information");

            return userInfos;
        }

        public IEnumerable<UserInfoDto> GetFilteredUserInfos(string name, string createdDate)
        {
            DateTime? date = null;
            if (!string.IsNullOrEmpty(createdDate))
                date = Convert.ToDateTime(createdDate);

            var filteredUserInfo = _userInfoRepository.Search(name, date);

            return filteredUserInfo;
        }
    }
}
