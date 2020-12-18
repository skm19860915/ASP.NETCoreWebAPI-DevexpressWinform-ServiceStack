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
    public class JobBidChatSessionsRepository : IRepository<JobBidChatSession>
    {
        private readonly XpertersContext _context;

        public JobBidChatSessionsRepository(XpertersContext context)
        {
            _context = context;
        }


        public void Add(JobBidChatSession item)
        {
            _context.JobBidChatSessions.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<JobBidChatSession> items)
        {
            throw new NotImplementedException();
        }

        public JobBidChatSession Get(Guid id)
        {
            return _context.JobBidChatSessions.First(x => x.Id == id);
        }

        public JobBidChatSession Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobBidChatSession> Get()
        {
            return _context.JobBidChatSessions;
        }

        public IQueryable<JobBidChatSession> Get(Expression<Func<JobBidChatSession, bool>> whereCondition)
        {
            return _context.JobBidChatSessions.Where(whereCondition);
        }
        public bool Exists(Expression<Func<JobBidChatSession, bool>> whereCondition)
        {
            return _context.JobBidChatSessions.Any(whereCondition);
        }
        public void Update(JobBidChatSession jobBidChatSession)
        {
            _context.Entry(jobBidChatSession).State = EntityState.Modified;
        }

        public IQueryable<JobBidChatSession> Include(Expression<Func<JobBidChatSession, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

    

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
