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
    public class AccountDetailRepository: IRepository<AccountDetail>
    {
        private readonly XpertersContext _context;
        public AccountDetailRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(AccountDetail item)
        {
            _context.AccountDetails.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<AccountDetail> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<AccountDetail, bool>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public AccountDetail Get(Guid id)
        {
            return _context.AccountDetails.First(x => x.Id == id);
        }

        public AccountDetail Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<AccountDetail> Get()
        {
            return _context.AccountDetails;
        }

        public IQueryable<AccountDetail> Get(Expression<Func<AccountDetail, bool>> whereCondition)
        {
            return _context.AccountDetails.Where(whereCondition);
        }

        public IQueryable<AccountDetail> Include(Expression<Func<AccountDetail, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountDetail item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
