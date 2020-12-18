using System;
using System.Collections.Generic;
using xperters.entities.Entities;
using xperters.entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace xperters.repositories
{
    public class SkillsRepository : IRepository<Skill>
    {
        private readonly XpertersContext _context;

        public SkillsRepository(XpertersContext context)
        {
            _context = context;
        }
        public void Add(Skill item)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<Skill> items)
        {
            throw new NotImplementedException();
        }

        public Skill Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Skill Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Skill> Get()
        {
            return _context.Skills;
        }

        public IQueryable<Skill> Get(Expression<Func<Skill, bool>> whereCondition)
        {
            return _context.Skills.Where<Skill>(whereCondition);
        }
        public bool Exists(Expression<Func<Skill, bool>> whereCondition)
        {
            return _context.Skills.Any(whereCondition);
        }
        public void Update(Skill skill)
        {
            _context.Entry(skill).State = EntityState.Modified;
        }

        public IQueryable<Skill> Include(Expression<Func<Skill, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

    

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
