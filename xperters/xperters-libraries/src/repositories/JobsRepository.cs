using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobsRepository : IRepository<Job>
    {
        private readonly XpertersContext _context;

        public JobsRepository(XpertersContext context)
        {
            _context = context;
        }

        public virtual Job Get(Guid jobId)
        {
            return _context.Jobs.SingleOrDefault(x => x.Id == jobId);
        }

        public Job Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Job> Get()
        {
            return _context.Jobs;
        }

        public IQueryable<Job> Get(Expression<Func<Job, bool>> whereCondition)
        {
            return _context.Jobs.Where(whereCondition);
        }

        public void Add(Job job)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();
        }

        public void AddList(List<Job> items)
        {
            throw new NotImplementedException();
        }
        public bool Exists(Expression<Func<Job, bool>> whereCondition)
        {
            return _context.Jobs.Any(whereCondition);
        }
        public void Update(Job job)
        {
            _context.Entry(job).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<Job> Include(Expression<Func<Job, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

   
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
