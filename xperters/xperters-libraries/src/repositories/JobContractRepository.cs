using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobContractRepository : IRepository<JobContract>
    {
        private readonly XpertersContext _context;

        public JobContractRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(JobContract item)
        {
            _context.JobContracts.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<JobContract> items)
        {
            throw new NotImplementedException();
        }

  
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<JobContract, bool>> whereCondition)
        {
            return _context.JobContracts.Any(whereCondition);
        }

        public JobContract Get(Guid id)
        {
            return _context.JobContracts.SingleOrDefault(x => x.Id == id);
        }

        public JobContract Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobContract> Get()
        {
            return _context.JobContracts;
        }

        public IQueryable<JobContract> Get(Expression<Func<JobContract, bool>> whereCondition)
        {
            return _context.JobContracts.Where<JobContract>(whereCondition);
        }

        public IQueryable<JobContract> Include(Expression<Func<JobContract, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(JobContract item)
        {
             _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
