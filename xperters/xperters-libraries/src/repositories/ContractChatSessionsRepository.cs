using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
   public class ContractChatSessionsRepository:IRepository<ContractChatSession>
    {
        private readonly XpertersContext _context;

        public ContractChatSessionsRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(ContractChatSession item)
        {
            _context.ContractChatSessions.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<ContractChatSession> items)
        {
            throw new NotImplementedException();
        }

        public ContractChatSession Get(Guid id)
        {
            return _context.ContractChatSessions.SingleOrDefault(x => x.Id == id);
        }

        public ContractChatSession Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatSession> Get()
        {
            return _context.ContractChatSessions;
        }

        public IQueryable<ContractChatSession> Get(Expression<Func<ContractChatSession, bool>> whereCondition)
        {
            return _context.ContractChatSessions.Where<ContractChatSession>(whereCondition);
        }
        public bool Exists(Expression<Func<ContractChatSession, bool>> whereCondition)
        {
            return _context.ContractChatSessions.Any(whereCondition);
        }

        public void Update(ContractChatSession item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatSession> Include(Expression<Func<ContractChatSession, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

    
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
