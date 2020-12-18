using System;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using xperters.business.Extensions;
using xperters.business.Interfaces;
using xperters.domain;
using xperters.entities.Entities;
using xperters.enums;
using xperters.extensions;
using xperters.repositories;

namespace xperters.business
{
    public class AccountManager : IAccountManager
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Country> _countriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountManager(IRepository<User> usersRepository, IRepository<Country> countriesRepository, IMapper mapper, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _countriesRepository = countriesRepository;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<AccountManager>();
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto GetUserById(string userId)
        {
            if (!Guid.TryParse(userId, out var id))
            {
                _logger.LogWarning($"User with id {userId} cannot be found. Exiting...");
                return null;
            }
            try
            {
                var user = _usersRepository.Get(x => x.Id == id)
                                            .Include(c=>c.Country)
                                            .FirstOrDefault();

                if (user != null)
                {
                    var userDto = _mapper.Map<UserDto>(user);

                    return userDto;
                }
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }

            return null;
        }

        public UserDto GetUserByEmail(string email)
        {

            var user = _usersRepository.Get(x=>x.Email == email).FirstOrDefault();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public ResultModel Login(UserDto userModel)
        {
            var resultModel = new ResultModel();
            return resultModel;
        }

        public UserDto GetSignedInUser()
        {

            return Policy.Handle<Exception>()
                .Retry(3, (e, i) =>
                {
                    _logger.LogError($"Error '{e.Message}' at retry #{i}");
                    throw new ArgumentNullException("no user found");
                })
                .Execute(() =>
                {
                    var a = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var defaultUser = _httpContextAccessor.HttpContext.User?.GetUserId();
                    if (defaultUser != null)
                    {
                        var user = _usersRepository.Get(Guid.Parse(defaultUser));
                        var userDto = _mapper.Map<UserDto>(user);
                        return userDto;
                    }

                    _logger.LogCritical("No user details found");
                    return null;
                });
        }

        public void CreateUser(UserDto user, string countryCode)
        {

            var dbUser = _usersRepository.Get(x => x.MobilePhone == user.MobilePhone || x.Id == user.Id).FirstOrDefault();

            if (dbUser == null)
            {
                // use default country as US
                var country = countryCode.IsNotBlank() ? 
                    _countriesRepository.Get(x => x.CountryCode.Equals(countryCode)).First() : 
                    _countriesRepository.Get(x => x.CountryCode.Equals("US")).First();

                var entity = _mapper.Map<User>(user);
                entity.CountryId = country.Id;
                _usersRepository.Add(entity);
            }
            else
            {
                _logger.Log(LogLevel.Warning, $"Somehow user {user.MobilePhone}, email: {user.Email} already exists");
            }
        }

        public void CheckAndUpdateUserCountry(UserDto user, string countryCode)
        {
            if (countryCode.IsBlank())
            {
                return;
            }

            var dbUser = _usersRepository.Get(x => x.MobilePhone == user.MobilePhone)
                                                                    .Include(c=>c.Country)
                                                                    .FirstOrDefault();

            if (dbUser != null && !dbUser.Country.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
            {
                
                // check if the user already exists - if not create it
                var country = _countriesRepository.Get(x => x.CountryCode.Equals(countryCode)).First();

                dbUser.CountryId = country.Id;
                _usersRepository.Add(dbUser);
            }
            else
            {
                _logger.Log(LogLevel.Warning, $"Somehow user {user.MobilePhone}, email: {user.Email} already exists");
            }
        }

        public ResultModel UpdateUserRole(Enums.UserRole role)
        {
            var resultModel = new ResultModel();
            var defaultUser = _httpContextAccessor.HttpContext.User?.GetUserId();
            var user = _usersRepository.Get(Guid.Parse(defaultUser));
            if (user != null && user.UserRole == 0)
            {
                user.UserRole = role;
                _usersRepository.Update(user);
                resultModel.Error = false;
            }
            else
            {
                resultModel.Error = false;
            }
           
            return resultModel;
        }
    }
}
