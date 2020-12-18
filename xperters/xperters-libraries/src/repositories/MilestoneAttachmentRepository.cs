using System;
using System.Collections.Generic;
using xperters.entities;
using xperters.entities.Entities;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace xperters.repositories
{
    public class MilestoneAttachmentRepository : IRepository<MilestoneAttachment>
    {
        private readonly XpertersContext _context;

        public MilestoneAttachmentRepository(XpertersContext context)
        {
            _context = context;
        }


        public void Add(MilestoneAttachment item)
        {
            _context.MilestoneAttachments.Add(item);
            _context.SaveChanges();
        }

        public void AddList(List<MilestoneAttachment> items)
        {
            _context.MilestoneAttachments.AddRange(items);
            _context.SaveChanges();
        }

        public bool Exists(Expression<Func<MilestoneAttachment, bool>> whereCondition)
        {
            return _context.MilestoneAttachments.Any(whereCondition);
        }

        public MilestoneAttachment Get(Guid id)
        {
            return _context.MilestoneAttachments.Find(id);
        }

        public MilestoneAttachment Get(string field)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MilestoneAttachment> Get()
        {
            return _context.MilestoneAttachments;
        }

        public IQueryable<MilestoneAttachment> Get(Expression<Func<MilestoneAttachment, bool>> whereCondition)
        {
            return _context.MilestoneAttachments.Where<MilestoneAttachment>(whereCondition);
        }

        public void Update(MilestoneAttachment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public IQueryable<MilestoneAttachment> Include(Expression<Func<MilestoneAttachment, object>> whereCondition)
        {
            var expression = (MemberExpression)whereCondition.Body;
            string PropertyName = expression.Member.Name;
            return _context.MilestoneAttachments.Include(PropertyName).AsQueryable<MilestoneAttachment>();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
