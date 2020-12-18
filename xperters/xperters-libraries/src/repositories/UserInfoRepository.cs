using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.domain;
using xperters.entities;
using xperters.entities.Entities;
using xperters.enums;

namespace xperters.repositories
{
    public class UserInfoRepository : IRepositoryReadOnly<UserInfoDto>
    {
        private readonly XpertersContext _context;
        private readonly IMapper _mapper;

        public UserInfoRepository(XpertersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UserInfoDto> Get(int page, int pageSize)
        {
            var users = _context.Users.Select(u => new
            {
                u.Id,
                u.CreatedDate,
                u.MobilePhone,
                u.UserRole,
                u.IsEnabled,
                u.CountryId,
                u.DisplayName,
                u.Jobs
            }).OrderByDescending(d => d.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var countries = _context.Countries;

            var result = (from u in users 
                          select new UserInfoDto
                          {
                              Id = u.Id,
                              DisplayName = u.DisplayName,
                              Mobile = u.MobilePhone,
                              UserRole = u.UserRole.GetDescription(),
                              Created = u.CreatedDate,
                              IsEnabled = u.IsEnabled,
                              Country = countries.FirstOrDefault(c => c.Id == u.CountryId).CountryName,
                              Jobs = GetMap(u.Jobs)
                          }).AsEnumerable();

            return result;
        }

        private List<JobDto> GetMap(IEnumerable<Job> jobs)
        {
            var list = new List<JobDto>();
            foreach (var job in jobs)
            {
                var record = _mapper.Map<JobDto>(job);
                list.Add(record);
            }

            return list;
        }

        public IEnumerable<UserInfoDto> Search(string title, DateTime? date)
        {
            var allUsers = _context.Users.Select(u => new
            {
                u.Id,
                u.CreatedDate,
                u.MobilePhone,
                u.UserRole,
                u.IsEnabled,
                u.CountryId,
                u.DisplayName,
                u.Jobs
            }).ToList();

            var countries = _context.Countries;

            if (!string.IsNullOrEmpty(title))
            {
                var titleFilteredUsers = allUsers.Where(u => u.DisplayName != null && u.DisplayName.Contains(title));
                if(date != null)
                    titleFilteredUsers = titleFilteredUsers.Where(x => DateTime.Equals(x.CreatedDate.Date, date.Value.Date));

                var result1 = (from u in titleFilteredUsers
                              select new UserInfoDto
                              {
                                  Id = u.Id,
                                  DisplayName = u.DisplayName,
                                  Mobile = u.MobilePhone,
                                  UserRole = u.UserRole.GetDescription(),
                                  Created = u.CreatedDate,
                                  IsEnabled = u.IsEnabled,
                                  Country = countries.FirstOrDefault(c => c.Id == u.CountryId).CountryName,
                                  Jobs = GetMap(u.Jobs)
                              }).AsEnumerable();

                return result1;
            }

            var dateFilteredUsers = allUsers.Where(x => DateTime.Equals(x.CreatedDate.Date, date.Value.Date));

            var result2 = (from u in dateFilteredUsers
                           select new UserInfoDto
                           {
                               Id = u.Id,
                               DisplayName = u.DisplayName,
                               Mobile = u.MobilePhone,
                               UserRole = u.UserRole.GetDescription(),
                               Created = u.CreatedDate,
                               IsEnabled = u.IsEnabled,
                               Country = countries.FirstOrDefault(c => c.Id == u.CountryId).CountryName,
                               Jobs = GetMap(u.Jobs)
                           }).AsEnumerable();
                              
            return result2;
        }

        public UserInfoDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserInfoDto> Get(Expression<Func<UserInfoDto, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<UserInfoDto, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }
    }
}
