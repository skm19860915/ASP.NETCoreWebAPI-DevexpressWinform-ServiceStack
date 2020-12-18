using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
   public class ContractChatSessionUsersRepository:IRepository<ContractChatSessionUser>
    {
        private readonly XpertersContext _context;

        public ContractChatSessionUsersRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(ContractChatSessionUser item)
        {
            _context.ContractChatSessionUsers.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<ContractChatSessionUser> items)
        {
            throw new NotImplementedException();
        }

        public ContractChatSessionUser Get(Guid id)
        {
            return _context.ContractChatSessionUsers.SingleOrDefault(x => x.Id == id);
        }

        public ContractChatSessionUser Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatSessionUser> Get()
        {
            return _context.ContractChatSessionUsers;
        }

        public IQueryable<ContractChatSessionUser> Get(Expression<Func<ContractChatSessionUser, bool>> whereCondition)
        {
            return _context.ContractChatSessionUsers.Where<ContractChatSessionUser>(whereCondition);
        }

        public bool Exists(Expression<Func<ContractChatSessionUser, bool>> whereCondition)
        {
            return _context.ContractChatSessionUsers.Any(whereCondition);
        }

        public void Update(ContractChatSessionUser item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatSessionUser> Include(Expression<Func<ContractChatSessionUser, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

      

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
