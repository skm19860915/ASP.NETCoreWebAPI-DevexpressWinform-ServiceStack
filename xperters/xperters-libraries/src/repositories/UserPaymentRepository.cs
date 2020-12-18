using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class UserPaymentRepository : IRepository<UserPayment>
    {
        private readonly XpertersContext _context;
        public UserPaymentRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(UserPayment userPayment)
        {
            _context.UserPayments.Add(userPayment);
            _context.SaveChanges();
        }

        public void AddList(List<UserPayment> items)
        {
            throw new NotImplementedException();
        }

        public UserPayment Get(Guid id)
        {

            return _context.UserPayments.First(x => x.Id == id);
        }

        public UserPayment Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserPayment> Get()
        {
            return _context.UserPayments;
        }

        public IQueryable<UserPayment> Get(Expression<Func<UserPayment, bool>> whereCondition)
        {
            return _context.UserPayments.Where(whereCondition);
        }

        public bool Exists(Expression<Func<UserPayment, bool>> whereCondition)
        {
            return _context.UserPayments.Any(whereCondition);
        }

        public void Update(UserPayment userPayment)
        {
            _context.Entry(userPayment).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<UserPayment> Include(Expression<Func<UserPayment, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            UserPayment userPayment = _context.UserPayments.Find(id);

            _context.UserPayments.Remove(userPayment);

            _context.SaveChanges();
        }

            
    }
}
