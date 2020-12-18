using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class EmailAuditRepository : IRepository<EmailAudit>
    {
        private readonly XpertersContext _context;
        public EmailAuditRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(EmailAudit item)
        {
            _context.EmailAudits.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<EmailAudit> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<EmailAudit, bool>> whereCondition)
        {
            return _context.EmailAudits.Any(whereCondition);
        }

        public EmailAudit Get(Guid id)
        {
            return _context.EmailAudits.SingleOrDefault(x => x.Id == id);
        }

        public EmailAudit Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EmailAudit> Get()
        {
            return _context.EmailAudits;
        }

        public IQueryable<EmailAudit> Get(Expression<Func<EmailAudit, bool>> whereCondition)
        {
            return _context.EmailAudits.Where<EmailAudit>(whereCondition);
        }

        public IQueryable<EmailAudit> Include(Expression<Func<EmailAudit, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(EmailAudit item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
