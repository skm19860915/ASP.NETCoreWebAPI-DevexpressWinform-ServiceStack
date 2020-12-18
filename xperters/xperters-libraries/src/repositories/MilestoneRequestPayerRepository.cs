using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
   public class MilestoneRequestPayerRepository : IRepository<MilestoneRequestPayer>
    {
        private readonly XpertersContext _context;
        public MilestoneRequestPayerRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(MilestoneRequestPayer item)
        {
            _context.MilestoneRequestPayers.Add(item);
            _context.SaveChanges();
        }
        public void AddList(List<MilestoneRequestPayer> items)
        {
            throw new NotImplementedException();
        }
        public bool Exists(Expression<Func<MilestoneRequestPayer, bool>> whereCondition)
        {
            return _context.MilestoneRequestPayers.Any(whereCondition);
        }
        public MilestoneRequestPayer Get(Guid id)
        {
            return _context.MilestoneRequestPayers.SingleOrDefault(x => x.Id == id);
        }
        public IQueryable<MilestoneRequestPayer> Get()
        {
            return _context.MilestoneRequestPayers;
        }
        public IQueryable<MilestoneRequestPayer> Get(Expression<Func<MilestoneRequestPayer, bool>> whereCondition)
        {
            return _context.MilestoneRequestPayers.Where<MilestoneRequestPayer>(whereCondition);
        }
        public void Update(MilestoneRequestPayer item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<MilestoneRequestPayer> Include(Expression<Func<MilestoneRequestPayer, object>> whereCondition)
        {
            throw new NotImplementedException();
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        MilestoneRequestPayer IRepository<MilestoneRequestPayer>.Get(Guid id)
        {
            return _context.MilestoneRequestPayers.SingleOrDefault(x => x.Id == id);
        }
        MilestoneRequestPayer IRepository<MilestoneRequestPayer>.Get(string field)
        {
            throw new NotImplementedException();
        }
        IQueryable<MilestoneRequestPayer> IRepository<MilestoneRequestPayer>.Get()
        {
            return _context.MilestoneRequestPayers;
        }
    }
}
