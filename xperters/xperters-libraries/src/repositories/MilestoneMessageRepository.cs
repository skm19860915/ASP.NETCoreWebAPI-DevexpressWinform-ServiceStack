using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class MilestoneMessageRepository: IRepository<MilestoneMessage>
    {
        private readonly XpertersContext _context;
        public MilestoneMessageRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(MilestoneMessage item)
        {

            _context.MilestoneMessage.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<MilestoneMessage> items)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<MilestoneMessage, bool>> whereCondition)
        {
            return _context.MilestoneMessage.Any(whereCondition);
        }

        public MilestoneMessage Get(Guid id)
        {
            return _context.MilestoneMessage.SingleOrDefault(x => x.Id == id);
        }

        public MilestoneMessage Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MilestoneMessage> Get()
        {
            return _context.MilestoneMessage;
        }

        public IQueryable<MilestoneMessage> Get(Expression<Func<MilestoneMessage, bool>> whereCondition)
        {
            return _context.MilestoneMessage.Where<MilestoneMessage>(whereCondition);
        }

        public void Update(MilestoneMessage item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<MilestoneMessage> Include(Expression<Func<MilestoneMessage, object>> whereCondition)
        {
            throw new NotImplementedException();
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
