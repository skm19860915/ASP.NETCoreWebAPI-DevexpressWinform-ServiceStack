using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
   public class MilestoneSystemRequestPayerRepository : IRepository<MilestoneSystemRequestPayer>
    {
        private readonly XpertersContext _context;
        public MilestoneSystemRequestPayerRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(MilestoneSystemRequestPayer item)
        {
            _context.MilestoneSystemRequestPayers.Add(item);
            _context.SaveChanges();
        }
        public void AddList(List<MilestoneSystemRequestPayer> items)
        {
            throw new NotImplementedException();
        }
        public bool Exists(Expression<Func<MilestoneSystemRequestPayer, bool>> whereCondition)
        {
            return _context.MilestoneSystemRequestPayers.Any(whereCondition);
        }
        public MilestoneSystemRequestPayer Get(Guid id)
        {
            return _context.MilestoneSystemRequestPayers.SingleOrDefault(x => x.Id == id);
        }
        public IQueryable<MilestoneSystemRequestPayer> Get()
        {
            return _context.MilestoneSystemRequestPayers;
        }
        public IQueryable<MilestoneSystemRequestPayer> Get(Expression<Func<MilestoneSystemRequestPayer, bool>> whereCondition)
        {
            return _context.MilestoneSystemRequestPayers.Where<MilestoneSystemRequestPayer>(whereCondition);
        }
        public void Update(MilestoneSystemRequestPayer item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<MilestoneSystemRequestPayer> Include(Expression<Func<MilestoneSystemRequestPayer, object>> whereCondition)
        {
            throw new NotImplementedException();
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        MilestoneSystemRequestPayer IRepository<MilestoneSystemRequestPayer>.Get(Guid id)
        {
            return _context.MilestoneSystemRequestPayers.SingleOrDefault(x => x.Id == id);
        }
        MilestoneSystemRequestPayer IRepository<MilestoneSystemRequestPayer>.Get(string field)
        {
            throw new NotImplementedException();
        }
        IQueryable<MilestoneSystemRequestPayer> IRepository<MilestoneSystemRequestPayer>.Get()
        {
            return _context.MilestoneSystemRequestPayers;
        }
    }
}
