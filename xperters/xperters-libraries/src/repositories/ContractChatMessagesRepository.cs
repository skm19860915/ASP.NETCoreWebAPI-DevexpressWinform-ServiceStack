using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
   public class ContractChatMessagesRepository:IRepository<ContractChatMessage>
    {
        private readonly XpertersContext _context;

        public ContractChatMessagesRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(ContractChatMessage item)
        {
            _context.ContractChatMessages.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<ContractChatMessage> items)
        {
            throw new NotImplementedException();
        }

        public ContractChatMessage Get(Guid id)
        {
            return _context.ContractChatMessages.SingleOrDefault(x => x.Id == id);
        }

        public ContractChatMessage Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatMessage> Get()
        {
            return _context.ContractChatMessages;
        }

        public IQueryable<ContractChatMessage> Get(Expression<Func<ContractChatMessage, bool>> whereCondition)
        {
            return _context.ContractChatMessages.Where<ContractChatMessage>(whereCondition);
        }
        public bool Exists(Expression<Func<ContractChatMessage, bool>> whereCondition)
        {
            return _context.ContractChatMessages.Any(whereCondition);
        }

        public void Update(ContractChatMessage item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContractChatMessage> Include(Expression<Func<ContractChatMessage, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
