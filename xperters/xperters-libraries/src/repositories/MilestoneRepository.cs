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
    public class MilestoneRepository : IRepository<Milestone>
    {
        private readonly XpertersContext _context;
        public MilestoneRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(Milestone item)
        {

            _context.Milestones.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<Milestone> items)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Milestone, bool>> whereCondition)
        {
            return _context.Milestones.Any(whereCondition);
        }

        public Milestone Get(Guid id)
        {
            return _context.Milestones.SingleOrDefault(x => x.Id == id);
        }

        public Milestone Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Milestone> Get()
        {
            return _context.Milestones;
        }

        public IQueryable<Milestone> Get(Expression<Func<Milestone, bool>> whereCondition)
        {
            return _context.Milestones.Where<Milestone>(whereCondition);
        }

        public void Update(Milestone item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<Milestone> Include(Expression<Func<Milestone, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

   
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
