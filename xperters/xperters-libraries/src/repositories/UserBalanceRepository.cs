using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class UserBalanceRepository : IRepository<UserBalance>
    {
        private readonly XpertersContext _context;
        public UserBalanceRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(UserBalance userBalance)
        {
            _context.UserBalances.Add(userBalance);
            _context.SaveChanges();
        }

        public void AddList(List<UserBalance> items)
        {
            throw new NotImplementedException();
        }

        public UserBalance Get(Guid id)
        {

            return _context.UserBalances.First(x => x.Id == id);
        }

        public UserBalance Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserBalance> Get()
        {
            return _context.UserBalances;
        }

        public IQueryable<UserBalance> Get(Expression<Func<UserBalance, bool>> whereCondition)
        {
            return _context.UserBalances.Where(whereCondition);
        }

        public bool Exists(Expression<Func<UserBalance, bool>> whereCondition)
        {
            return _context.UserBalances.Any(whereCondition);
        }

        public void Update(UserBalance userBalance)
        {
            _context.Entry(userBalance).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<UserBalance> Include(Expression<Func<UserBalance, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            UserBalance userBalance = _context.UserBalances.Find(id);

            _context.UserBalances.Remove(userBalance);

            _context.SaveChanges();
        }

            
    }
}
