using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class AccountsRepository : IRepository<User>
    {
        private readonly XpertersContext _context;
        public AccountsRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void AddList(List<User> items)
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Get()
        {
            return _context.Users;
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> whereCondition)
        {
            return _context.Users.Where(whereCondition);
        }

        public bool Exists(Expression<Func<User, bool>> whereCondition)
        {
            return _context.Users.Any(whereCondition);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<User> Include(Expression<Func<User, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
