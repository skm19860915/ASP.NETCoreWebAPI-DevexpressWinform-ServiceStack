using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class UserWithdrawalsRepository : IRepository<UserWithdrawal>
    {
        private readonly XpertersContext _context;
        public UserWithdrawalsRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(UserWithdrawal item)
        {
            _context.UserWithdrawals.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<UserWithdrawal> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<UserWithdrawal, bool>> whereCondition)
        {
            return _context.UserWithdrawals.Any(whereCondition);
        }

        public UserWithdrawal Get(Guid id)
        {
            return _context.UserWithdrawals.SingleOrDefault(x => x.Id == id);
        }

        public UserWithdrawal Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserWithdrawal> Get()
        {
            return _context.UserWithdrawals;
        }

        public IQueryable<UserWithdrawal> Get(Expression<Func<UserWithdrawal, bool>> whereCondition)
        {
            return _context.UserWithdrawals.Where<UserWithdrawal>(whereCondition);
        }

        public IQueryable<UserWithdrawal> Include(Expression<Func<UserWithdrawal, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(UserWithdrawal item)
        {
            throw new NotImplementedException();
        }
    }
}
