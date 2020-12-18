using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobBidChatSessionUsersRepository : IRepository<JobBidChatSessionUser>
    {

        private readonly XpertersContext _context;

        public JobBidChatSessionUsersRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(JobBidChatSessionUser item)
        {
            _context.JobBidChatSessionUsers.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<JobBidChatSessionUser> items)
        {
            throw new NotImplementedException();
        }

        public JobBidChatSessionUser Get(Guid id)
        {
            return _context.JobBidChatSessionUsers.First(x => x.Id == id);
        }

        public JobBidChatSessionUser Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobBidChatSessionUser> Get()
        {
            return _context.JobBidChatSessionUsers;
        }

        public IQueryable<JobBidChatSessionUser> Get(Expression<Func<JobBidChatSessionUser, bool>> whereCondition)
        {
            return _context.JobBidChatSessionUsers.Where(whereCondition);
        }
        public bool Exists(Expression<Func<JobBidChatSessionUser, bool>> whereCondition)
        {
            return _context.JobBidChatSessionUsers.Any(whereCondition);
        }
        public void Update(JobBidChatSessionUser jobBidChatSessionUser)
        {
            _context.Entry(jobBidChatSessionUser).State = EntityState.Modified;
        }

        public IQueryable<JobBidChatSessionUser> Include(Expression<Func<JobBidChatSessionUser, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

      

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
