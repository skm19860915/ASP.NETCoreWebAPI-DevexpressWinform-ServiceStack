using System;
using System.Collections.Generic;
using System.Text;
using xperters.entities;
using xperters.entities.Entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace xperters.repositories
{
    public class JobBidAttachmentRepository: IRepository<JobBidAttachment>
    {
        private readonly XpertersContext _context;

        public JobBidAttachmentRepository(XpertersContext context)
        {
            _context = context;
        }

        public void Add(JobBidAttachment jobBid)
        {
            _context.JobBidAttachments.Add(jobBid);
            _context.SaveChanges();
        }

        public void AddList(List<JobBidAttachment> items)
        {
            _context.JobBidAttachments.AddRange(items);
            _context.SaveChanges();
        }

        public JobBidAttachment Get(Guid id)
        {
            return _context.JobBidAttachments.Find(id);
        }

        public JobBidAttachment Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<JobBidAttachment> Get()
        {
            return _context.JobBidAttachments;
        }

        public IQueryable<JobBidAttachment> Get(Expression<Func<JobBidAttachment, bool>> whereCondition)
        {
            return _context.JobBidAttachments.Where<JobBidAttachment>(whereCondition);
        }
        public bool Exists(Expression<Func<JobBidAttachment, bool>> whereCondition)
        {
            return _context.JobBidAttachments.Any(whereCondition);
        }
        public void Update(JobBidAttachment jobBidAttachment)
        {
            _context.Entry(jobBidAttachment).State = EntityState.Modified;
        }

        public IQueryable<JobBidAttachment> Include(Expression<Func<JobBidAttachment, object>> whereCondition)
        {
            throw new NotImplementedException();
        }

     
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
