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
    public class ContractMilestoneFundRepository : IRepository<ContractMilestoneFund>
    {
        private readonly XpertersContext _context;
        public ContractMilestoneFundRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(ContractMilestoneFund item)
        {
            _context.ContractMilestoneFunds.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<ContractMilestoneFund> items)
        {
            throw new NotImplementedException();
        }


        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<ContractMilestoneFund, bool>> whereCondition)
        {
            return _context.ContractMilestoneFunds.Any(whereCondition);
        }

        public ContractMilestoneFund Get(Guid id)
        {
            return _context.ContractMilestoneFunds.SingleOrDefault(x => x.Id == id);
        }

        public ContractMilestoneFund Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractMilestoneFund> Get()
        {
            return _context.ContractMilestoneFunds;
        }

        public IQueryable<ContractMilestoneFund> Get(Expression<Func<ContractMilestoneFund, bool>> whereCondition)
        {
            return _context.ContractMilestoneFunds.Where<ContractMilestoneFund>(whereCondition);
        }

        public IQueryable<ContractMilestoneFund> Include(Expression<Func<ContractMilestoneFund, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(ContractMilestoneFund item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
