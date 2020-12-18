using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using xperters.entities;
using xperters.entities.Entities;

namespace xperters.repositories
{
    public class JobAttachmentsRepository : IRepository<JobAttachment>
    {
        private readonly XpertersContext _context;

        public JobAttachmentsRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(JobAttachment item)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<JobAttachment> items)
        {            
            _context.JobAttachments.AddRange(items);
            _context.SaveChanges();
        }

        public JobAttachment Get(Guid id)
        {
            return _context.JobAttachments.Find(id);
        }

        public JobAttachment Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobAttachment> Get()
        {
            return _context.JobAttachments;
        }

        public IQueryable<JobAttachment> Get(Expression<Func<JobAttachment, bool>> whereCondition)
        {
            return _context.JobAttachments.Where<JobAttachment>(whereCondition);
        }
        public bool Exists(Expression<Func<JobAttachment, bool>> whereCondition)
        {
            return _context.JobAttachments.Any(whereCondition);
        }
        public void Update(JobAttachment jobAttachment)
        {
            _context.Entry(jobAttachment).State = EntityState.Modified;
        }

        public IQueryable<JobAttachment> Include(Expression<Func<JobAttachment, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

    

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
