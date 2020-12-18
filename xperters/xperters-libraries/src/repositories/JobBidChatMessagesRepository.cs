using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobBidChatMessagesRepository : IRepository<JobBidChatMessage>
    {
        private readonly XpertersContext _context;

        public JobBidChatMessagesRepository(XpertersContext context)
        {
            _context = context;
        }


        public void Add(JobBidChatMessage item)
        {
            _context.JobBidChatMessages.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<JobBidChatMessage> items)
        {
            throw new NotImplementedException();
        }

        public JobBidChatMessage Get(Guid id)
        {
            return _context.JobBidChatMessages.First(x => x.Id == id);
        }

        public JobBidChatMessage Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobBidChatMessage> Get()
        {
            return _context.JobBidChatMessages;
        }

        public IQueryable<JobBidChatMessage> Get(Expression<Func<JobBidChatMessage, bool>> whereCondition)
        {
            return _context.JobBidChatMessages.Where(whereCondition);
        }
        public bool Exists(Expression<Func<JobBidChatMessage, bool>> whereCondition)
        {
            return _context.JobBidChatMessages.Any(whereCondition);
        }
        public void Update(JobBidChatMessage jobBidChatMessages)
        {
            _context.Entry(jobBidChatMessages).State = EntityState.Modified;
        }

        public IQueryable<JobBidChatMessage> Include(Expression<Func<JobBidChatMessage, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
