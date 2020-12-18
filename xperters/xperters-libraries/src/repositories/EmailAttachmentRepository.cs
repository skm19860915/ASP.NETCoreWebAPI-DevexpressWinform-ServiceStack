using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class EmailAttachmentRepository : IRepository<EmailAttachments>
    {
        private readonly XpertersContext _context;

        public EmailAttachmentRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(EmailAttachments item)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<EmailAttachments> items)
        {
            _context.EmailAttachments.AddRange(items);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<EmailAttachments, bool>> whereCondition)
        {
            return _context.EmailAttachments.Any(whereCondition);
        }

        public EmailAttachments Get(Guid id)
        {
            return _context.EmailAttachments.Find(id);
        }

        public EmailAttachments Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EmailAttachments> Get()
        {
            return _context.EmailAttachments;
        }

        public IQueryable<EmailAttachments> Get(Expression<Func<EmailAttachments, bool>> whereCondition)
        {
            return _context.EmailAttachments.Where<EmailAttachments>(whereCondition);
        }

        public IQueryable<EmailAttachments> Include(Expression<Func<EmailAttachments, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

        public void Update(EmailAttachments item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
