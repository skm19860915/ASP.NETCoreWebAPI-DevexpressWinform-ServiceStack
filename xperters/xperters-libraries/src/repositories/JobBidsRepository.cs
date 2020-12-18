using System;
using System.Collections.Generic;
using System.Text;
using xperters.entities;
using xperters.entities.Entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace xperters.repositories
{
    public class JobBidsRepository: IRepository<JobBid>
    {
        private readonly XpertersContext _context;

        public JobBidsRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(JobBid jobBid)
        {
            _context.JobBids.Add(jobBid);
            _context.SaveChanges();
        }

        public void AddList(List<JobBid> items)
        {
            throw new NotImplementedException();
        }

        public JobBid Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public JobBid Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobBid> Get()
        {
            return _context.JobBids;
        }

        public IQueryable<JobBid> Get(Expression<Func<JobBid, bool>> whereCondition)
        {
            return _context.JobBids.Where<JobBid>(whereCondition);
        }
        public bool Exists(Expression<Func<JobBid, bool>> whereCondition)
        {
            return _context.JobBids.Any(whereCondition);
        }
        public void Update(JobBid jobBid)
        {
            _context.Entry(jobBid).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IQueryable<JobBid> Include(Expression<Func<JobBid, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
